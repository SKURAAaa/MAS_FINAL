namespace MAS_FINAL.Models
{
    // Klasa Pracownik dziedziczy po klasie Osoby, przechowuje dane osobowe.
    // Dzięki niej ograniczamy powtarzanie się tych samych pól w innych klasach.
    public class Employee : Person
    {
        public string Role { get; set; }
        public DateTime HireDate { get; set; }
        public int Salary { get; set; }
        public List<Animal> AssignedAnimals { get; set; }

        public Employee()
        {
            AssignedAnimals = new List<Animal>();
        }

        // // Metoda do przypisywania wybiegów pracownikom
        // public void SprzatajWybiegi(Wybieg wybieg)
        // {
        //     // Logika sprzątania wybiegów
        // }
    }
}