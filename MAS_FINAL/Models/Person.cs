namespace MAS_FINAL.Models
{
    // Klasa abstrakcyjna Person reprezentuje ogólne cechy wspólne dla wszystkich osób w systemie,
    // ale sama nie jest instancjonowana. Konkretne klasy, takie jak Pracownik, będą dziedziczyć po tej klasie
    // i implementować specyficzne funkcjonalności.
    public abstract class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}