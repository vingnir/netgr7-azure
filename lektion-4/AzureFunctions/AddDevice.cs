using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureFunctions.Models;
using Microsoft.Azure.Devices;

namespace AzureFunctions
{
    public static class AddDevice
    {
        private static readonly RegistryManager registryManager = RegistryManager.CreateFromConnectionString(Environment.GetEnvironmentVariable("ServiceClient"));

        [FunctionName("AddDevice")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "devices")] HttpRequest req,
            ILogger log)
        {
           

            var model = JsonConvert.DeserializeObject<DeviceModel>(await new StreamReader(req.Body).ReadToEndAsync());

            var device = await registryManager.AddDeviceAsync(new Device(model.Id));
            var twin = await registryManager.GetTwinAsync(device.Id);

            twin.Properties.Desired["placement"] = model.Placement;
            await registryManager.UpdateTwinAsync(twin.DeviceId, twin, twin.ETag);

            return new OkObjectResult(device.Authentication.SymmetricKey.PrimaryKey);
        }
    }
}
