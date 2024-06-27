using System.Linq;
using System.Windows;
using MAS_FINAL.Models;
using MAS_FINAL.Services;

namespace MAS_FINAL
{
    public partial class VeterinarianWindow : Window
    {
        private ZooManagementSystem zooManagementSystem;
        private Animal selectedAnimal;
        private string animalFilePath = "C:\\Users\\Kacper\\RiderProjects\\MAS_FINAL\\MAS_FINAL\\animal.json";

        public VeterinarianWindow(ZooManagementSystem system)
        {
            InitializeComponent();
            zooManagementSystem = system;
            LoadAnimals();
        }

        private void LoadAnimals()
        {
            AnimalListBox.ItemsSource = zooManagementSystem.GetAnimals().Select(a => $"{a.Species} ({a.Age} lat)");
        }

        private void AnimalListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (AnimalListBox.SelectedItem != null)
            {
                var selectedAnimalName = AnimalListBox.SelectedItem.ToString().Split('(')[0].Trim();
                selectedAnimal = zooManagementSystem.GetAnimals().FirstOrDefault(a => a.Species == selectedAnimalName);

                if (selectedAnimal != null)
                {
                    VaccinationTextBox.Text = string.Join("\n", selectedAnimal.History.Vaccinations);
                    DiseaseHistoryTextBox.Text = string.Join("\n", selectedAnimal.History.Illnesses);
                }
            }
        }

        private void IssuePrescription_Click(object sender, RoutedEventArgs e)
        {
            if (selectedAnimal == null)
            {
                MessageBox.Show("Proszę wybrać zwierzę z listy.");
                return;
            }

            string medication = Microsoft.VisualBasic.Interaction.InputBox("Podaj nazwę leku:", "Wystaw receptę", "");
            string dosage = Microsoft.VisualBasic.Interaction.InputBox("Podaj dawkowanie:", "Wystaw receptę", "");

            if (!string.IsNullOrEmpty(medication) && !string.IsNullOrEmpty(dosage))
            {
                string prescription = $"{medication} - Dawkowanie: {dosage}";
                selectedAnimal.History.Illnesses.Add(prescription);
                MessageBox.Show($"Recepta została wystawiona dla {selectedAnimal.Species}.");

                // Aktualizacja wyświetlanych danych
                DiseaseHistoryTextBox.Text = string.Join("\n", selectedAnimal.History.Illnesses);
            }
            else
            {
                MessageBox.Show("Nie podano wszystkich danych.");
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                zooManagementSystem.SaveAnimalsToFile(animalFilePath);
                MessageBox.Show("Zmiany zostały zapisane.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas zapisywania zmian: {ex.Message}");
            }
        }
    }
}
