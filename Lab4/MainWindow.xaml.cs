using Lab4.Forms;
using Lab4.Models;
using Lab4.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;  
using System.Linq;                

namespace Lab4  
{
    public partial class MainWindow : Window
    {
        private MeasurementChannel _channel;
        private ObservableCollection<Device> _viewDevices = new();
        private const string FileName = "channel.json";

        public MainWindow()
        {
            InitializeComponent();

            if (File.Exists(FileName))
            {
                _channel = JsonStorage.Load(FileName);
            }
            else
            {
                _channel = new MeasurementChannel();
            }

            // ВИКОРИСТОВУЄМО Devices (ObservableCollection) для UI
            _viewDevices = new ObservableCollection<Device>(_channel.Devices);
            DevicesListBox.ItemsSource = _viewDevices;

            EditButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;

            Closing += MainWindow_Closing;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            DeviceForm form = new DeviceForm();
            if (form.ShowDialog() == true)
            {
                AddDeviceThroughPlainCollection(form.CreatedDevice);
            }
        }

        private void AddDeviceThroughPlainCollection(Device device)
        {
            // Отримуємо plain список з _channel (якщо метод існує)
            // Якщо метода GetPlainList() немає, використовуємо:
            List<Device> plainList = _channel.Devices.ToList();
            plainList.Add(device);

            // Оновлюємо канал з plain списку (якщо метод існує)
            // Якщо метода UpdateFromPlainList() немає, використовуємо:
            _channel.Devices.Clear();
            foreach (var d in plainList)
            {
                _channel.Devices.Add(d);
            }

            RefreshDevices();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (DevicesListBox.SelectedItem is not Device device)
                return;

            MessageBoxResult result = MessageBox.Show(
                $"Видалити пристрій \"{device.SerialNumber}\"?",
                "Підтвердження видалення",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Якщо є метод RemoveDevice, використовуємо його
                // _channel.RemoveDevice(device);
                // Інакше:
                _channel.Devices.Remove(device);
                RefreshDevices();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (DevicesListBox.SelectedItem is not Device device)
                return;

            Device copy = new Device(device);
            DeviceForm form = new DeviceForm(copy);

            if (form.ShowDialog() == true)
            {
                MessageBoxResult result = MessageBox.Show(
                    "Зберегти зміни?",
                    "Підтвердження",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result != MessageBoxResult.Yes)
                    return;

                // Робота з plain списком
                List<Device> plainList = _channel.Devices.ToList();
                int index = plainList.IndexOf(device);

                if (index >= 0)
                {
                    plainList[index] = form.CreatedDevice;

                    // Оновлюємо канал
                    _channel.Devices.Clear();
                    foreach (var d in plainList)
                    {
                        _channel.Devices.Add(d);
                    }
                }

                RefreshDevices();
            }
        }

        private void RefreshDevices()
        {
            _viewDevices.Clear();
            foreach (Device device in _channel.Devices)
            {
                _viewDevices.Add(device);
            }

            // Оновлюємо заголовок вікна з інформацією про канал
            this.Title = $"Лабораторна робота - Канал вимірювань";
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            JsonStorage.Save(_channel, FileName);
        }

        private void DevicesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool selected = DevicesListBox.SelectedItem != null;
            EditButton.IsEnabled = selected;
            DeleteButton.IsEnabled = selected;
        }
    }
}