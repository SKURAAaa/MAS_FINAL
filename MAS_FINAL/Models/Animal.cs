using System.Collections.Generic;

namespace MAS_FINAL.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string Species { get; set; }
        public int Age { get; set; }
        public string HealthStatus { get; set; }
        public AnimalHistory History { get; set; }
        public Enclosure Enclosure { get; set; } // Dodanie referencji do wybiegu

        // Kompozycja pomiędzy klasą Animal a klasą Zoo. Animal należy do Zoo.
        public Zoo Zoo { get; set; }
        
        public Animal()
        {
            History = new AnimalHistory();
        }
    }

    public class AnimalHistory
    {
        public List<string> Vaccinations { get; set; }
        public List<string> Illnesses { get; set; }

        public AnimalHistory()
        {
            Vaccinations = new List<string>();
            Illnesses = new List<string>();
        }
    }
}