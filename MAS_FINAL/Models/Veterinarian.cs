namespace MAS_FINAL.Models
{
    // Klasa Weterynarz dziedziczy po klasie Pracownik. Jest to przykład overlapping.
    public class Veterinarian : Employee
    {
        // Specyficzne atrybuty i metody dla Weterynarza
        public string Specialization { get; set; }

        public void IssuePrescription(string prescription)
        {
            // Metoda specyficzna dla Weterynarza
        }
    }
}