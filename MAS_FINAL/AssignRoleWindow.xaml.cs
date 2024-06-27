using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MAS_FINAL.Models;
using MAS_FINAL.Services;

namespace MAS_FINAL
{
    public partial class AssignRoleWindow : Window
    {
        private ZooManagementSystem zooManagementSystem;
        private string employeeFilePath = "employees.json"; // Ścieżka do pliku

        public AssignRoleWindow(ZooManagementSystem system)
        {
            InitializeComponent();
            zooManagementSystem = system;
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            EmployeeListBox.ItemsSource = zooManagementSystem.GetEmployees().Select(e => $"{e.Id} - {e.FirstName} {e.LastName} - Rola: {e.Role}");
        }

        private void SearchEmployeeById_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(EmployeeIdTextBox.Text, out int employeeId))
            {
                var employee = zooManagementSystem.GetEmployeeById(employeeId);
                if (employee != null)
                {
                    EmployeeListBox.ItemsSource = new[] { $"{employee.Id} - {employee.FirstName} {employee.LastName} - Rola: {employee.Role}" };
                }
                else
                {
                    MessageBox.Show("Nie znaleziono pracownika.");
                }
            }
            else
            {
                MessageBox.Show("Proszę wprowadzić poprawne ID pracownika.");
            }
        }

        private void AssignRole_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeListBox.SelectedItem == null || RoleComboBox.SelectedItem == null)
            {
                MessageBox.Show("Proszę wybrać pracownika i rolę.");
                return;
            }

            var selectedEmployeeInfo = EmployeeListBox.SelectedItem.ToString();
            var selectedEmployeeId = int.Parse(selectedEmployeeInfo.Split('-')[0].Trim());
            var selectedRole = (RoleComboBox.SelectedItem as ComboBoxItem).Content.ToString();

            var employee = zooManagementSystem.GetEmployeeById(selectedEmployeeId);

            if (employee != null)
            {
                employee.Role = selectedRole;
                MessageBox.Show($"Rola {selectedRole} została przypisana do {employee.FirstName} {employee.LastName}.");
                
                // Zapisanie zmian do pliku
                zooManagementSystem.SaveEmployeesToFile(employeeFilePath);
                LoadEmployees();
            }
            else
            {
                MessageBox.Show("Nie znaleziono pracownika.");
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                zooManagementSystem.SaveEmployeesToFile(employeeFilePath);
                MessageBox.Show("Zmiany zostały zapisane.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas zapisywania zmian: {ex.Message}");
            }
        }
    }
}
