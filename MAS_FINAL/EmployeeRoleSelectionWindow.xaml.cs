using System.Windows;
using MAS_FINAL.Services;

namespace MAS_FINAL
{
    public partial class EmployeeRoleSelectionWindow : Window
    {
        private ZooManagementSystem zooManagementSystem;

        public EmployeeRoleSelectionWindow(ZooManagementSystem system)
        {
            InitializeComponent();
            zooManagementSystem = system;
        }

        private void Employee_Click(object sender, RoutedEventArgs e)
        {
            // Logika dla zalogowania jako pracownik
            // Przykładowe wyświetlenie listy zwierząt
            var animalSearchWindow = new AnimalSearchWindow(zooManagementSystem.GetAnimals());
            animalSearchWindow.Show();
            this.Close();
        }

        private void Manager_Click(object sender, RoutedEventArgs e)
        {
            // Logika dla zalogowania jako kierownik
            var assignRoleWindow = new AssignRoleWindow(zooManagementSystem);
            assignRoleWindow.Show();
            this.Close();
        }
        private void Veterinarian_Click(object sender, RoutedEventArgs e)
        {
            // Logika dla zalogowania jako weterynarz
            var veterinarianWindow = new VeterinarianWindow(zooManagementSystem);
            veterinarianWindow.Show();
            this.Close();
        }
    }
}