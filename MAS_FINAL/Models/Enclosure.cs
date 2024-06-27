namespace MAS_FINAL.Models
{
    public class Enclosure
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public List<Animal> Animals { get; set; } // Lista zwierząt na wybiegu

        public Enclosure()
        {
            Animals = new List<Animal>();
        }

        // Generowanie raportu jakie zwierzęta są na wybiegu
        public string GenerujRaportJakieZwierzętaSąNaWybiegu()
        {
            return string.Join(", ", Animals.Select(a => a.Species));
        }
    }
}