using System.Linq;
using System.Windows;
using MAS_FINAL.Models;
using MAS_FINAL.Services;

namespace MAS_FINAL
{
    public partial class LoginWindow : Window
    {
        private ZooManagementSystem zooManagementSystem;
        public Customer LoggedInCustomer { get; private set; }
        public bool IsGuest { get; private set; }

        public LoginWindow(ZooManagementSystem system)
        {
            InitializeComponent();
            zooManagementSystem = system;
        }

        private void CustomerLogin_Click(object sender, RoutedEventArgs e)
        {
            string firstName = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;

            var customer = zooManagementSystem.GetCustomers().FirstOrDefault(c => c.FirstName == firstName && c.LastName == lastName);
            if (customer != null)
            {
                LoggedInCustomer = customer;
                IsGuest = false;
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Nie znaleziono klienta.");
            }
        }

        private void EmployeeLogin_Click(object sender, RoutedEventArgs e)
        {
            var roleSelectionWindow = new EmployeeRoleSelectionWindow(zooManagementSystem);
            roleSelectionWindow.Show();
            this.Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string firstName = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;

            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                var newCustomer = new Customer
                {
                    Id = zooManagementSystem.GenerateCustomerId(),
                    FirstName = firstName,
                    LastName = lastName,
                    Ticket = null
                };

                zooManagementSystem.AddCustomer(newCustomer);
                MessageBox.Show("Rejestracja zakończona sukcesem. Możesz teraz zalogować się jako klient.");
            }
            else
            {
                MessageBox.Show("Proszę wprowadzić zarówno imię, jak i nazwisko.");
            }
        }

        private void ContinueAsGuest_Click(object sender, RoutedEventArgs e)
        {
            IsGuest = true;
            this.DialogResult = true;
        }
    }
}
