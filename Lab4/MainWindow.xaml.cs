using System.Windows;
using Lab4.Models;

namespace Lab4
{
    public partial class MainWindow : Window
    {
        private MeasurementChannel _channel;

        public MainWindow()
        {
            InitializeComponent();

            _channel = new MeasurementChannel();

            RefreshList();
        }

        private void RefreshList()
        {
            DevicesListBox.ItemsSource = null;
            DevicesListBox.ItemsSource = _channel.Devices;
        }
    }
}