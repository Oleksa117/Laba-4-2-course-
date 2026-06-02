using System.Collections.ObjectModel;

namespace Lab4.Models
{
    public class MeasurementChannel
    {
        public ObservableCollection<Device> Devices { get; set; }

        public MeasurementChannel()
        {
            Devices = new ObservableCollection<Device>();
        }
    }
}