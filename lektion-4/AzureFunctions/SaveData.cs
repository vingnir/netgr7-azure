using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventHubs;
using System.Text;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureFunctions.Models;

namespace AzureFunctions
{
    public class SaveData
    {
        
        [FunctionName("SaveData")]
        public void Run(
            [IoTHubTrigger("messages/events", Connection = "IotHub", ConsumerGroup = "azurefunctions")]EventData message, 
            [CosmosDB(databaseName: "NETGR7", collectionName: "StoredMessages", ConnectionStringSetting = "CosmosDb", CreateIfNotExists = true)] out dynamic cosmos,
            
            ILogger log)
        {

            try
            {
                var data = JsonConvert.DeserializeObject<DeviceMessageModel>(Encoding.UTF8.GetString(message.Body));
                data.DeviceId = message.SystemProperties["iothub-connection-device-id"].ToString();

                cosmos = data;
            }
            catch
            {
                cosmos = null;
            }
            
        }
    }
}