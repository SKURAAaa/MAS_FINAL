using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MAS_FINAL.Models;

namespace MAS_FINAL
{
    public partial class AnimalSearchWindow : Window
    {
        private List<Animal> animals;

        public AnimalSearchWindow(List<Animal> animals)
        {
            InitializeComponent();
            this.animals = animals;
        }

        private void LoadAnimals(IEnumerable<Animal> animalsToLoad)
        {
            AnimalList.Items.Clear();
            foreach (var animal in animalsToLoad)
            {
                AnimalList.Items.Add($"{animal.Species} (wiek: {animal.Age})");
            }
            AnimalList.Visibility = animalsToLoad.Any() ? Visibility.Visible : Visibility.Collapsed;
        }

        private void FilterSearch_Click(object sender, RoutedEventArgs e)
        {
            var filteredAnimals = animals.AsEnumerable();

            if (!string.IsNullOrEmpty(SearchBox.Text))
            {
                filteredAnimals = filteredAnimals.Where(a => a.Species.Contains(SearchBox.Text, StringComparison.OrdinalIgnoreCase));
            }

            if (SpeciesFilter.SelectedIndex > 0)
            {
                var species = (SpeciesFilter.SelectedItem as ComboBoxItem).Content.ToString();
                filteredAnimals = filteredAnimals.Where(a => a.Species.Equals(species, StringComparison.OrdinalIgnoreCase));
            }

            if (AgeFilter.SelectedIndex > 0)
            {
                var ageRange = (AgeFilter.SelectedItem as ComboBoxItem).Content.ToString();
                filteredAnimals = ageRange switch
                {
                    "0-5" => filteredAnimals.Where(a => a.Age >= 0 && a.Age <= 5),
                    "6-10" => filteredAnimals.Where(a => a.Age >= 6 && a.Age <= 10),
                    "11-15" => filteredAnimals.Where(a => a.Age >= 11 && a.Age <= 15),
                    _ => filteredAnimals
                };
            }

            LoadAnimals(filteredAnimals);
        }
    }
}
