using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IotDevice.Models
{
    public class DeviceModel
    {
        public string Id { get; set; }
        public string SensorType { get; set; }
        public string SharedAccessKey { get; set; }
    }
}
