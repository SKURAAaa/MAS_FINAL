namespace MAS_FINAL.Models
{
    public class Zoo
    {
        private List<Animal> animals;
        private List<Employee> employees;
        private Dictionary<int, Employee> employeeMap; // Kwalifikator PracownikID

        public Zoo()
        {
            animals = new List<Animal>();
            employees = new List<Employee>();
            employeeMap = new Dictionary<int, Employee>(); // Inicjalizacja kwalifikatora
        }

        // Metody dodawania i usuwania zwierząt // Kompozycja pomiędzy klasą Zoo a klasą Animal. Zoo nie istnieje bez zwierząt.
        public void DodajZwierze(Animal animal)
        {
            animals.Add(animal);
        }

        public void UsunZwierze(Animal animal)
        {
            animals.Remove(animal);
        }

        // Metody dodawania i usuwania pracowników
        public void DodajPracownika(Employee employee)
        {
            employees.Add(employee);
            employeeMap[employee.Id] = employee; // Użycie kwalifikatora do szybkiego dostępu
        }

        public void UsunPracownika(Employee employee)
        {
            employees.Remove(employee);
            employeeMap.Remove(employee.Id); // Usunięcie z kwalifikatora
        }

        // Znajdowanie pracownika po ID za pomocą kwalifikatora Asocjacja z kwalifikatorem
        public Employee ZnajdzPracownikaPoId(int employeeId)
        {
            if (employeeMap.ContainsKey(employeeId))
            {
                return employeeMap[employeeId]; // Szybki dostęp za pomocą kwalifikatora
            }
            return null;
        }

        // Pobieranie listy pracowników
        public List<Employee> GetEmployees()
        {
            return employees;
        }
    }
}