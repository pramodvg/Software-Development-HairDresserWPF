using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HairDresserApp2;



namespace HairDresserWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SortedDictionary<int, string> availableSlots = new SortedDictionary<int, string>();
        Operations operations = new Operations();
        uint readAge;
        decimal readHeight;
        long creditCardNumber;
        string gender = string.Empty;
        private ObservableCollection<Appointment> appointments = null;

        public ObservableCollection<Appointment> Appointments { get => appointments; set => appointments = value; }
        public MainWindow()
        {
            InitializeComponent();
            Appointments = new ObservableCollection<Appointment>();
        }


        private void bookAppointment_Click(object sender, RoutedEventArgs e)
        {    
            //validate input fields
            bool flag = validateFields(timeSlotMenu.Text, nameInput.Text, ageInput.Text, heightInput.Text, creditCardInput.Text, gender);
            if (flag)
            {
                // Services according to categories
                string services = buildServices();
                // Write data into Binary File
                operations.WriteToXmlFile(timeSlotMenu.Text, nameInput.Text, readAge, readHeight, creditCardInput.Text, gender,services);
                // removal of chosen timeslot from the dropdown list
                timeSlotMenu.Items.Remove(timeSlotMenu.SelectedItem);
                // If Combobox is empty Disable the Book Appointment Button
                if (timeSlotMenu.Items.Count <= 0)
                {
                    bookAppointment.IsEnabled = false;
                }
                resetForm();
                MessageBox.Show("Appointment Booked ✔");
            }
        
        }

        // Services according to categories
        private string buildServices()
        {
            StringBuilder availedServices = new StringBuilder();

            if ((bool)commonCheckBox.IsChecked)
            {
                availedServices.Append("Hair - Trim, Dye, Wash! ");
            }
            if ((bool)ladiesCheckBox.IsChecked && (bool)ChildrenCheckBox.IsChecked)
            {
                availedServices.Append("Styling for Occasions with sensitive trimmer & adjustable seat! ");
            }
            if ((bool)gentlemenCheckBox.IsChecked && (bool)ChildrenCheckBox.IsChecked)
            {
                availedServices.Append("Trim Beard & Moustache with sensitive trimmer & adjustable seat! ");
            }
            if ((bool)ladiesCheckBox.IsChecked && !(bool)ChildrenCheckBox.IsChecked)
            {
                availedServices.Append("Styling for Occasions! ");
            }
            if ((bool)gentlemenCheckBox.IsChecked && !(bool)ChildrenCheckBox.IsChecked)
            {
                availedServices.Append("Trim Beard & Trim moustache! ");
            }
            if((bool)ChildrenCheckBox.IsChecked && !(bool)ladiesCheckBox.IsChecked && !(bool)gentlemenCheckBox.IsChecked)
            {
                availedServices.Append("Sensitive trimmer & adjustable seat! ");
            }

        return availedServices.ToString();
        }

        // Event handler for View Appointment Button
        private void ViewAppointment_Click(object sender, RoutedEventArgs e)
        {

            // Read from Binary File and populating Appointment List
            BookedAppointments bookedAppointments = operations.ReadFromBinFile();

            var query = from appointment in bookedAppointments.bookedAppointmentsList.Cast<Appointment>()
                        orderby appointment.Customer1.CustomerAge
                        select appointment;

            grdPeople.ItemsSource = query;

            /* viewBox.Text = String.Empty;
             sortLabel.Visibility = Visibility.Visible;*/

            // Read from Binary File and populating Appointment List
            /* BookedAppointments bookedAppointments = operations.ReadFromBinFile("AppointmentsFile.txt");
             int i=1;
             foreach(Appointment appointment in bookedAppointments){

                 Customer client = (Customer)appointment.Customer;

                 // Category String for Display 
                 string categoryStr = (appointment.Category == Category.Children) ? (client.CustomerGender.Equals("M") ? "Male Child" : "Female Child") : (client.CustomerGender.Equals("M") ? "Gentleman" : "Ladies");

                 string row = string.Format($"{i}) Time Slot: {appointment.TimeStamp}, Name : {client.CustomerName}, Age: {client.CustomerAge}," +
                                  $" Height: {client.CustomerHeight}, Category: {categoryStr}, credit card: {client.CustomerCreditCardNumber},\n    Services Availed: {appointment.AvailedServices}");

                 viewBox.Text = viewBox.Text + row + "\n\n";
                 i++;
             }*/
        }

        // Handler for Radio Buttons change event
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            switch (rb.Name)
            {
                case "male":
                    gender = "M";
                    break;
                case "female":
                    gender = "F";
                    break;
                default:
                    break;
            }
            // Dynamic Handling (enable/disable) Checkboxs according to age and gender selection
            handledCheckBoxes();
        }


        // Function To validate every inputs
        private bool validateFields(string timeSlot, string name, string age, string height, string creditCard, string gender)
        {

            bool flag = true;


            if (timeSlot.Equals(""))
            {
                timeslotErrorLabel.Visibility = Visibility.Visible;
                flag = false;
            }
            else
            {
                timeslotErrorLabel.Visibility = Visibility.Hidden;
            }
            if ((name.Length == 0) || (name.Length > 50))
            {
                nameInput.BorderBrush = Brushes.Red;
                nameInput.Foreground = Brushes.Red;
                nameErrorLabel.Visibility = Visibility.Visible;
                flag = false;
            }
            else
            {
                nameInput.BorderBrush = Brushes.LightGray;
                nameErrorLabel.Visibility = Visibility.Hidden;
               
            }
            if (!uint.TryParse(age, out readAge) || (readAge < 1) || (readAge > 100))
            {
                ageInput.BorderBrush = Brushes.Red;
                ageInput.Foreground = Brushes.Red;
                ageErrorLabel.Visibility = Visibility.Visible;
                flag = false;
            }
            else
            {
                ageInput.BorderBrush = Brushes.LightGray;
                ageErrorLabel.Visibility = Visibility.Hidden;
                
            }
            if (!decimal.TryParse(height, out readHeight) || (readHeight < 1) || (readHeight > 10))
            {
                heightInput.BorderBrush = Brushes.Red;
                heightInput.Foreground = Brushes.Red;
                heightErrorLabel.Visibility = Visibility.Visible;
                flag = false;
            }
            else
            {
                heightInput.BorderBrush = Brushes.LightGray;
                heightErrorLabel.Visibility = Visibility.Hidden;
                
            }
            if (!long.TryParse(creditCard, out creditCardNumber) || (creditCard.Length != 16))
            {

                creditCardInput.BorderBrush = Brushes.Red;
                creditCardInput.Foreground = Brushes.Red;
                creditCardErrorLabel.Visibility = Visibility.Visible;
                flag = false;
            }
            else
            {
                creditCardInput.BorderBrush = Brushes.LightGray;
                creditCardErrorLabel.Visibility = Visibility.Hidden;
                
            }
            if (!(gender.Equals("M") || gender.Equals("F")))
            {

                genderErrorLabel.Visibility = Visibility.Visible;
                flag = false;
            }
            else
            {
                genderErrorLabel.Visibility = Visibility.Hidden;
                
            }

            if ((commonCheckBox.IsChecked == false) && (gentlemenCheckBox.IsChecked == false) && (ladiesCheckBox.IsChecked == false) && (ChildrenCheckBox.IsChecked == false))
            {
                serviceErrorText.Visibility = Visibility.Visible;
                flag = false;
            }
            else
            {
                serviceErrorText.Visibility = Visibility.Hidden;
            }
            return flag;
        }



        // Fuction for form default state
        private void resetForm()
        {
            timeSlotMenu.SelectedIndex = 0;
            nameInput.Text = string.Empty;
            ageInput.Text = string.Empty;
            heightInput.Text = string.Empty;
            creditCardInput.Text = string.Empty;
            male.IsChecked = true;
            female.IsChecked = false;
            viewBox.Text = string.Empty;
            commonCheckBox.IsChecked = true;
            ChildrenCheckBox.IsEnabled = false;
            ladiesCheckBox.IsEnabled = false;
            gentlemenCheckBox.IsEnabled = false;
            ChildrenCheckBox.IsChecked = false;
            ladiesCheckBox.IsChecked = false;
            gentlemenCheckBox.IsChecked = false;
            sortLabel.Visibility = Visibility.Hidden;
        }

        // Dynamic Handling (enable/disable) Checkboxs according to age and gender selection
        private void handledCheckBoxes()
        {
            if (ageInput.Text.Length > 0)
            {
                uint age = 0;

                if (uint.TryParse(ageInput.Text, out age) && (age <= 100))
                {
                    if (age <= 18)
                    {
                        if (gender.Equals("F"))
                        {
                            ladiesCheckBox.IsEnabled = true;
                            gentlemenCheckBox.IsEnabled = false;
                            gentlemenCheckBox.IsChecked = false;
                        }
                        else
                        {
                            gentlemenCheckBox.IsEnabled = true;
                            ladiesCheckBox.IsEnabled = false;
                            ladiesCheckBox.IsChecked = false;
                        }
                        ChildrenCheckBox.IsEnabled = true;
                    }
                    else
                    {
                        if (gender.Equals("F"))
                        {
                            ladiesCheckBox.IsEnabled = true;
                            gentlemenCheckBox.IsEnabled = false;
                            gentlemenCheckBox.IsChecked = false;

                        }
                        else
                        {
                            gentlemenCheckBox.IsEnabled = true;
                            ladiesCheckBox.IsEnabled = false;
                            ladiesCheckBox.IsChecked = false;

                        }
                        ChildrenCheckBox.IsEnabled = false;
                        ChildrenCheckBox.IsChecked = false;

                    }
                }
            }
           
        }

        // Handling checkboxes on "OnkeyUp" event of ageInput
        private void ageOnKeyUp(object sender, KeyEventArgs e)
        {
            handledCheckBoxes();
        }

        private void onTextChange(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Foreground = Brushes.Black;
            textBox.BorderBrush = Brushes.LightGray;
        }
    }
}