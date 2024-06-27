using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MAS_FINAL.Models;
using Newtonsoft.Json;

namespace MAS_FINAL.Services
{
    public class ZooManagementSystem
    {
        private List<Animal> animals;
        private List<Employee> employees;
        private List<Customer> customers;
        private List<Product> products;
        private List<Purchase> purchases; // Asocjacja z atrybutem - lista zakupów
        private int nextCustomerId;

        public ZooManagementSystem()
        {
            animals = new List<Animal>();
            employees = new List<Employee>();
            customers = new List<Customer>();
            products = new List<Product>();
            purchases = new List<Purchase>(); // Inicjalizacja listy zakupów
            nextCustomerId = 1;

            // Wczytanie danych klientów z pliku
            string customerFilePath = "customers.json";
            LoadCustomersFromFile(customerFilePath);
            
            string employeeFilePath = "employees.json";
            LoadEmployeesFromFile(employeeFilePath);
        }

        // Kompozycja pomiędzy klasą Zoo a klasą Animal. Zoo nie istnieje bez zwierząt.
        public void AddAnimal(Animal animal)
        {
            animals.Add(animal);
        }

        public List<Animal> GetAnimals()
        {
            return animals;
        }

        public void AddEmployee(Employee employee)
        {
            employees.Add(employee);
        }

        public List<Employee> GetEmployees()
        {
            return employees;
        }

        public void AddCustomer(Customer customer)
        {
            if (!customers.Any(c => c.Id == customer.Id))
            {
                customers.Add(customer);
            }
        }

        public List<Customer> GetCustomers()
        {
            return customers;
        }

        public int GenerateCustomerId()
        {
            return nextCustomerId++;
        }

        // Sprzedaż biletu z informacją o zakupie
        public void SellTicket(int customerId, string ticketType)
        {
            var customer = customers.FirstOrDefault(c => c.Id == customerId);
            if (customer != null)
            {
                Ticket ticket = ticketType == "Specjalny" ? new SpecialTicket() : new NormalTicket();
                customer.BuyTicket(ticket); // Ustawiamy typ biletu dla klienta

                // Dodanie informacji o zakupie
                var purchase = new Purchase
                {
                    ProductId = GenerateTicketId(),
                    CustomerId = customerId,
                    Quantity = 1,
                    PurchaseDate = DateTime.Now,
                    ProductName = ticket.TicketType,
                    ProductPrice = ticket.Price
                };
                customer.Purchases.Add(purchase);
                purchases.Add(purchase); // Dodanie zakupu do listy zakupów

                UpdateCustomer(customer); // Aktualizacja klienta
                SendEmailConfirmation(customer);
            }
            else
            {
                throw new ArgumentException("Invalid customer ID");
            }
        }

        public int GenerateTicketId()
        {
            return new Random().Next(1000, 9999);
        }


        private void SendEmailConfirmation(Customer customer)
        {
            // Logika wysyłania e-maila tutaj
        }

        public void SaveCustomersToFile(string filePath)
        {
            try
            {
                var customerData = customers.Select(c => new
                {
                    c.Id,
                    c.FirstName,
                    c.LastName,
                    TicketType = c.Ticket?.TicketType,
                    Purchases = c.Purchases.Select(p => new
                    {
                        p.ProductId,
                        p.ProductName,
                        p.ProductPrice,
                        p.Quantity,
                        p.PurchaseDate
                    }).ToList()
                }).ToList();
                var jsonData = JsonConvert.SerializeObject(customerData, Formatting.Indented);
                File.WriteAllText(filePath, jsonData);
                Console.WriteLine("Dane klientów zostały zapisane do pliku: " + filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd podczas zapisywania danych klientów: " + ex.Message);
            }
        }

        public void LoadCustomersFromFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    var jsonData = File.ReadAllText(filePath);
                    var customerData = JsonConvert.DeserializeObject<List<Customer>>(jsonData);
                    customers = customerData ?? new List<Customer>();
                    Console.WriteLine("Dane klientów zostały wczytane z pliku: " + filePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd podczas wczytywania danych klientów: " + ex.Message);
            }
        }

        public void SaveAnimalsToFile(string filePath)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(animals, Formatting.Indented);
                File.WriteAllText(filePath, jsonData);
                Console.WriteLine("Dane zwierząt zostały zapisane do pliku: " + filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd podczas zapisywania danych zwierząt: " + ex.Message);
            }
        }

        public void LoadAnimalsFromFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    var jsonData = File.ReadAllText(filePath);
                    animals = JsonConvert.DeserializeObject<List<Animal>>(jsonData) ?? new List<Animal>();
                    Console.WriteLine("Dane zwierząt zostały wczytane z pliku: " + filePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd podczas wczytywania danych zwierząt: " + ex.Message);
            }
        }

        // Metoda do wczytywania danych pracowników z pliku
        public void LoadEmployeesFromFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    var jsonData = File.ReadAllText(filePath);
                    var employeeData = JsonConvert.DeserializeObject<List<dynamic>>(jsonData);
                    employees = employeeData.Select(ed =>
                    {
                        Employee employee;
                        if (ed.Role == "Caretaker")
                        {
                            employee = new Caretaker
                            {
                                Skills = ed.Skills
                            };
                        }
                        else if (ed.Role == "Veterinarian")
                        {
                            employee = new Veterinarian
                            {
                                Specialization = ed.Specialization
                            };
                        }
                        else
                        {
                            employee = new Employee();
                        }

                        employee.Id = ed.Id;
                        employee.FirstName = ed.FirstName;
                        employee.LastName = ed.LastName;
                        employee.HireDate = ed.HireDate;
                        employee.Salary = ed.Salary;
                        employee.Role = ed.Role;
                        return employee;
                    }).ToList();
                    Console.WriteLine("Dane pracowników zostały wczytane z pliku: " + filePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd podczas wczytywania danych pracowników: " + ex.Message);
            }
        }

        // Metoda do zapisywania danych pracowników do pliku
        public void SaveEmployeesToFile(string filePath)
        {
            try
            {
                Console.WriteLine("Rozpoczęto zapisywanie danych pracowników do pliku: " + filePath);

                // Sprawdzenie, czy mamy prawa do zapisu
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Plik nie istnieje. Zostanie utworzony nowy plik.");
                }

                var employeeData = employees.Select(e => new
                {
                    e.Id,
                    e.FirstName,
                    e.LastName,
                    e.HireDate,
                    e.Salary,
                    e.Role,
                    Skills = e is Caretaker caretaker ? caretaker.Skills : null,
                    Specialization = e is Veterinarian veterinarian ? veterinarian.Specialization : null
                }).ToList();

                var jsonData = JsonConvert.SerializeObject(employeeData, Formatting.Indented);
                Console.WriteLine("Zserializowane dane pracowników: " + jsonData); // Logowanie danych JSON

                File.WriteAllText(filePath, jsonData);
                Console.WriteLine("Dane pracowników zostały zapisane do pliku: " + filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd podczas zapisywania danych pracowników: " + ex.Message);
            }
        }

        // Dodanie nowego zakupu - Asocjacja z atrybutem pomiędzy klasami Customer i Product
        public void AddPurchase(int customerId, int productId, int quantity)
        {
            var customer = customers.FirstOrDefault(c => c.Id == customerId);
            var product = products.FirstOrDefault(p => p.Id == productId);

            if (customer == null || product == null)
            {
                throw new ArgumentException("Invalid customer or product ID");
            }

            var purchase = new Purchase
            {
                CustomerId = customerId,
                ProductId = productId,
                Quantity = quantity,
                PurchaseDate = DateTime.Now,
                ProductName = product.Name,
                ProductPrice = product.Price
            };

            customer.Purchases.Add(purchase);
            purchases.Add(purchase);
        }

        // Metoda do szybkiego wyszukiwania pracownika po jego unikalnym identyfikatorze
        public Employee GetEmployeeById(int employeeId)
        {
            return employees.FirstOrDefault(e => e.Id == employeeId);
        }

        // Metoda do aktualizacji klienta
        public void UpdateCustomer(Customer updatedCustomer)
        {
            var customer = customers.FirstOrDefault(c => c.Id == updatedCustomer.Id);
            if (customer != null)
            {
                customer.FirstName = updatedCustomer.FirstName;
                customer.LastName = updatedCustomer.LastName;
                customer.Ticket = updatedCustomer.Ticket;
                customer.Purchases = updatedCustomer.Purchases;
            }
            else
            {
                customers.Add(updatedCustomer);
            }
        }
    }
}