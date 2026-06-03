using Lab4.DTO;
using Lab4.Models;
using Lab4.DTO;
using Lab4.Models;
using System.IO;
using System.Text.Json;

namespace Lab4.Services
{
    public static class JsonStorage
    {
        public static void Save(MeasurementChannel channel,string path)
        {
            MeasurementChannelDto dto =Mapper.ToDto(channel);

            string json =JsonSerializer.Serialize(dto,new JsonSerializerOptions{WriteIndented = true});

            File.WriteAllText(path, json);
        }

        public static MeasurementChannel Load(string path)
        {
            string json = File.ReadAllText(path);

            MeasurementChannelDto? dto = JsonSerializer.Deserialize<MeasurementChannelDto>(json);

            if (dto == null)
                throw new InvalidDataException("Помилка десеріалізації.");

            MeasurementChannel channel = Mapper.FromDto(dto);

            // Перевірка після десеріалізації
            foreach (var device in channel.Devices)
            {
                device.Validate();
            }

            return channel;
        }
    }
}