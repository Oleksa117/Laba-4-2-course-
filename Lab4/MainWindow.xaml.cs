using Lab4.Forms;
using Lab4.Models;
using Lab4.Services;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

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
            List<Device> plainList = _channel.Devices.ToList();
            plainList.Add(device);

            _channel.Devices.Clear();

            foreach (Device d in plainList)
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
                {
                    return;
                }

                List<Device> plainList = _channel.Devices.ToList();

                int index = plainList.IndexOf(device);

                if (index >= 0)
                {
                    plainList[index] = form.CreatedDevice;
                }

                _channel.Devices.Clear();

                foreach (Device d in plainList)
                {
                    _channel.Devices.Add(d);
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
        }


        private void MainWindow_Closing(object sender,System.ComponentModel.CancelEventArgs e)
        {
            JsonStorage.Save(_channel,FileName);
        }

        private void DevicesListBox_SelectionChanged(object sender,System.Windows.Controls.SelectionChangedEventArgs e)
        {
            bool selected = DevicesListBox.SelectedItem != null;

            EditButton.IsEnabled = selected;
            DeleteButton.IsEnabled = selected;
        }
    }
}