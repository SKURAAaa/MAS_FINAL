namespace MAS_FINAL.Models
{
    // Klasa Opiekun dziedziczy po klasie Pracownik. Jest to przykład overlapping.
    public class Caretaker : Employee
    {
        // Specyficzne atrybuty i metody dla Opiekuna
        public string Skills { get; set; }

        public void TakeCareOfAnimals()
        {
            // Metoda specyficzna dla Opiekuna
        }
    }
}