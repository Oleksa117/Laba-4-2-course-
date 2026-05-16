using System.Collections.Generic;

namespace Lab4.Models
{
    public class MeasurementChannel
    {
        public List<Device> Devices { get; set; }

        public MeasurementChannel()
        {
            Devices = new List<Device>();
        }
    }
}