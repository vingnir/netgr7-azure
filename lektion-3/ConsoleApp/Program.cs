using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Newtonsoft.Json;
using System.Text;

namespace ConsoleApp
{
    public class Program
    {
        private static DeviceClient deviceClient = DeviceClient.CreateFromConnectionString("HostName=netgr7-iothub.azure-devices.net;DeviceId=ConsoleApp;SharedAccessKey=bapy1Nqwl8+lVSFR46jwgPM6ewabn6HU7961UGdwgEc=");
        private static Random rnd = new Random();

        public static async Task Main()
        {
            await UpdateReportedAsync("sensorType", "Console App");

            while (true)
            {
                await SendMessageAsync();
                await Task.Delay(10 * 1000);
            }
        }


        public static async Task SendMessageAsync()
        {
            var temperature = rnd.Next(0, 30);
            var temperatureAlert = false;

            if (temperature > 25)
            {
                temperatureAlert = true;
                Console.WriteLine($"Sending message to Azure IOT Hub ({DateTime.Now}) - WARNING! Temperature is to HIGH ({temperature})");
            }           
            else
            {
                temperatureAlert = false;
                Console.WriteLine($"Sending message  to Azure IOT Hub ({DateTime.Now}) ");
            }

            await UpdateReportedAsync("alertNotification", temperatureAlert);
            await deviceClient.SendEventAsync(new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new { temperature = temperature }))));
        }

        private static async Task UpdateReportedAsync(string property, object value)
        {
            TwinCollection reportedProperties = new TwinCollection();
            reportedProperties[property] = value;

            await deviceClient.UpdateReportedPropertiesAsync(reportedProperties);
        }
    }
}