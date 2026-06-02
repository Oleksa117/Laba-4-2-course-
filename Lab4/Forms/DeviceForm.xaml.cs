using Lab4.Forms;
using Lab4.Models;
using System;
using System.Windows;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace Lab4.Forms
{
    public partial class DeviceForm : Window
    {
        private Sensor _sensor;

        public Device CreatedDevice { get; private set; }

        public DeviceForm()
        {
            InitializeComponent();

            CalibrationDatePicker.SelectedDate = DateTime.Today;
        }

        public DeviceForm(Device device) : this()
        {
            _sensor = device.Sensor;
            PositionTextBox.Text = device.MountingPosition.ToString();
            CalibrationDatePicker.SelectedDate = device.CalibrationDate;
            SerialNumberTextBox.Text = device.SerialNumber;
        }

        private void SensorButton_Click(object sender, RoutedEventArgs e)
        {
            SensorForm form = new SensorForm();

            if (form.ShowDialog() == true)
            {
                _sensor = form.CreatedSensor;

                MessageBox.Show("Датчик створено.");
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_sensor == null)
                {
                    MessageBox.Show("Спочатку створіть датчик.");
                    return;
                }

                int position = int.Parse(PositionTextBox.Text);

                DateTime calibrationDate = CalibrationDatePicker.SelectedDate.Value;

                string serialNumber = SerialNumberTextBox.Text;

                CreatedDevice = new Device(_sensor, position, calibrationDate, serialNumber);

                var context = new ValidationContext(CreatedDevice);
                var results = new List<ValidationResult>();

                if (!Validator.TryValidateObject(CreatedDevice, context, results, true))
                {
                    MessageBox.Show(string.Join("\n", results.Select(r => r.ErrorMessage)), "Помилка валідації");
                    return;
                }

                DialogResult = true;
                Close();
            }
            catch
            {
                MessageBox.Show("Перевірте правильність введених даних.");
            }
        }
    }
}