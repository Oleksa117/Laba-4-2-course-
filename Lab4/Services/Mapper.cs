using Lab4.DTO;
using Lab4.Models;
using Lab4.DTO;
using Lab4.Models;
using System.Linq;

namespace Laba4.Services
{
    public static class Mapper
    {
        public static MeasurementChannelDto ToDto(MeasurementChannel channel)
        {
            return new MeasurementChannelDto
            {
                Devices = channel.Devices.Select(d => new DeviceDto
                    {
                        MountingPosition = d.MountingPosition,
                        CalibrationDate = d.CalibrationDate,

                        Sensor = new SensorDto
                        {
                            QuantityType = (int)d.Sensor.QuantityType,

                            MinValue = d.Sensor.MinValue,

                            MaxValue = d.Sensor.MaxValue,

                            CurrentValue = d.Sensor.CurrentValue
                        }
                    }).ToList()
            };
        }

        public static MeasurementChannel FromDto(MeasurementChannelDto dto)
        {
            MeasurementChannel channel = new MeasurementChannel();

            foreach (var d in dto.Devices)
            {
                Sensor sensor = new Sensor(
                    (QuantityType)d.Sensor.QuantityType,
                    d.Sensor.MinValue,
                    d.Sensor.MaxValue,
                    d.Sensor.CurrentValue);

                Device device = new Device(sensor,d.MountingPosition,d.CalibrationDate);

                channel.Devices.Add(device);
            }

            return channel;
        }
    }
}