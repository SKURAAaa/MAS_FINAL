using System.Linq;
using System.Windows;
using MAS_FINAL.Services;
using MAS_FINAL.Models;
using Microsoft.Win32;

namespace MAS_FINAL
{
    public partial class MainWindow : Window
    {
        private ZooManagementSystem zooManagementSystem;
        private Customer currentCustomer;
        private Employee currentEmployee;

        public MainWindow()
        {
            InitializeComponent();
            zooManagementSystem = new ZooManagementSystem();
        }

        private void LoadData_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                zooManagementSystem.LoadCustomersFromFile(openFileDialog.FileName);
                MessageBox.Show("Dane klientów zostały wczytane.");
            }
        }

        private void LoadAnimals_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                zooManagementSystem.LoadAnimalsFromFile(openFileDialog.FileName);
                MessageBox.Show("Dane zwierząt zostały wczytane.");
            }
        }

        private void SaveData_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                string customerFilePath = saveFileDialog.FileName;
                zooManagementSystem.SaveCustomersToFile(customerFilePath);
                MessageBox.Show("Dane klientów zostały zapisane do pliku: " + customerFilePath);
            }
        }

        private void ClientLogin_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow(zooManagementSystem);
            if (loginWindow.ShowDialog() == true)
            {
                if (loginWindow.IsGuest)
                {
                    currentCustomer = null; // Gość
                }
                else
                {
                    currentCustomer = loginWindow.LoggedInCustomer;
                }

                var ticketPurchaseWindow = new TicketPurchaseWindow(zooManagementSystem, currentCustomer);
                ticketPurchaseWindow.TicketPurchased += TicketPurchaseWindow_TicketPurchased;
                ticketPurchaseWindow.Show();
            }
        }

        private void TicketPurchaseWindow_TicketPurchased(object sender, EventArgs e)
        {
            var animalSearchWindow = new AnimalSearchWindow(zooManagementSystem.GetAnimals());
            animalSearchWindow.Show();
        }

        private void EmployeeLogin_Click(object sender, RoutedEventArgs e)
        {
            var roleSelectionWindow = new EmployeeRoleSelectionWindow(zooManagementSystem);
            roleSelectionWindow.Show();
        }

        private void GuestLogin_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow(zooManagementSystem);
            if (loginWindow.ShowDialog() == true)
            {
                currentCustomer = loginWindow.LoggedInCustomer;
                var ticketPurchaseWindow = new TicketPurchaseWindow(zooManagementSystem, currentCustomer);
                ticketPurchaseWindow.TicketPurchased += TicketPurchaseWindow_TicketPurchased;
                ticketPurchaseWindow.Show();
            }
        }
    }
}
