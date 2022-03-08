using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ClassLibrary.Models;
using Microsoft.Azure.Devices;

namespace AzureFunctions
{
    public static class AddDevice
    {
        private static readonly RegistryManager registryManager = RegistryManager.CreateFromConnectionString(Environment.GetEnvironmentVariable("IotHubConnection"));

        [FunctionName("AddDevice")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            var data = JsonConvert.DeserializeObject<DeviceAddModel>(await new StreamReader(req.Body).ReadToEndAsync());

            if(!string.IsNullOrEmpty(data.PinCode))
            {
                var device = await registryManager.AddDeviceAsync(new Device(data.DeviceId));
                var twin = await registryManager.GetTwinAsync(device.Id);

                twin.Properties.Desired["placement"] = data.Placement;
                await registryManager.UpdateTwinAsync(twin.DeviceId, twin, twin.ETag);
            }


            return new OkResult();
        }
    }
}
