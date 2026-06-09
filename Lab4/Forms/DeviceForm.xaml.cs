using Lab4.Forms;
using Lab4.Models;
using System;
using System.Windows;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace Lab4.Forms
{
    public partial class DeviceForm : Window//Пристрій
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
            _sensor = device.Sensor;//копіює датчик
            PositionTextBox.Text = device.MountingPosition.ToString();//заповнює 
            CalibrationDatePicker.SelectedDate = device.CalibrationDate;
            SerialNumberTextBox.Text = device.SerialNumber;
        }

        // Відкриває форму для створення датчика 
        private void SensorButton_Click(object sender, RoutedEventArgs e)
        {
            SensorForm form = new SensorForm();//ств форм

            if (form.ShowDialog() == true)
            {
                _sensor = form.CreatedSensor;//збер ств датчик

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

                var context = new ValidationContext(CreatedDevice);//перевіряє валідність даних (помилки)
                var results = new List<ValidationResult>();

                if (!Validator.TryValidateObject(CreatedDevice, context, results, true))//перевіряє об'єкт на відповідність всім атрибутам валідації
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