using Lab4.DTO;
using System.Collections.Generic;

namespace Lab4.DTO
{
    public class MeasurementChannelDto
    {
        public List<DeviceDto> Devices { get; set; } = new();//ініціалізація порожнього списку для уникнення null
        public int ChannelNumber { get; set; }
    }
}