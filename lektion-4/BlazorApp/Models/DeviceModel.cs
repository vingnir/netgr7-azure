namespace BlazorApp.Models
{
    public class DeviceModel
    {
        public string Id { get; set; }
        public string ConnectionState { get; set; }
        public string SensorType { get; set; }
        public string Placement { get; set; }
        public bool AlertNotification { get; set; }
    }
}
