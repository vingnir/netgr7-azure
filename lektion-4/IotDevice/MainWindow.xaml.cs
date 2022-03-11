using Dapper;
using IotDevice.Models;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IotDevice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DeviceModel model;
        private Random rnd;
        private DeviceClient deviceClient;
        
        private string hostName = "netgr7-iothub.azure-devices.net";
        private string apiUrl = "https://netgr7-azurefunction.azurewebsites.net/api/devices";
        private string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HansMattin-Lassei\Documents\Utbildning\NETGR7\lektion-4\IotDevice\device_db.mdf;Integrated Security=True;Connect Timeout=30";
        
        private bool isConfigured = false;

        public MainWindow()
        {
            InitializeComponent();
            InitializeDevice();
 
            if(!isConfigured)
            {
                btnConnect.IsEnabled = true;
                btnConnect.Content = "Connect";
            }
            else
            {
                ConnectAndSendAsync().GetAwaiter();
            }
        }

        private void InitializeDevice()
        {
            rnd = new();
            model = new();

            using (IDbConnection conn = new SqlConnection(sql))
            {
                conn.Open();
                var data = conn.Query<DeviceModel>("SELECT * FROM Device");

                if (data.Any())
                {
                    foreach (var device in data)
                    {
                        model.Id = device.Id;
                        model.SensorType = device.SensorType;
                        model.SharedAccessKey = device.SharedAccessKey;
                    }

                    isConfigured = true;
                }
            }
        }

        private async void btnConnect_Click(object sender, RoutedEventArgs e)
        {

            using (IDbConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HansMattin-Lassei\Documents\Utbildning\NETGR7\lektion-4\IotDevice\device_db.mdf;Integrated Security=True;Connect Timeout=30"))
            {

                model.Id = Guid.NewGuid().ToString();
                model.SensorType = "GUI APP";

                using (var http = new HttpClient())
                {
                    var result = await http.PostAsJsonAsync(apiUrl, model);
                    model.SharedAccessKey = await result.Content.ReadAsStringAsync();
                }

                conn.Open();
                await conn.ExecuteAsync("INSERT INTO Device VALUES(@Id, @SensorType, @SharedAccessKey)", model);

            }

            await ConnectAndSendAsync();
        }

        private async Task ConnectAndSendAsync()
        {
            tblockDeviceId.Text = model.Id;
            btnConnect.IsEnabled = false;
            btnConnect.Content = "Connected";

            deviceClient = DeviceClient.CreateFromConnectionString($"HostName={hostName};DeviceId={model.Id};SharedAccessKey={model.SharedAccessKey}");
            await UpdateSensorTypeAsync(model.SensorType);
            await SendMessageAsync();
        }

        private async Task UpdateSensorTypeAsync(string sensorType)
        {
            TwinCollection reportedProperty = new TwinCollection();
            reportedProperty["sensorType"] = sensorType;

            await deviceClient.UpdateReportedPropertiesAsync(reportedProperty);
        }

        private async Task SendMessageAsync()
        {
            while (true)
            {
                var message = new DeviceMessage
                {
                    Temperature = rnd.Next(20, 30),
                    Humidity = rnd.Next(30, 40)
                };

                await deviceClient.SendEventAsync(new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message))));
                tblockInfo.Text = $"Message sent {DateTime.Now}";

                await Task.Delay(60 * 1000);
            }
        }
    }
}
