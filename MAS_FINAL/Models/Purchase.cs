using System;

namespace MAS_FINAL.Models
{
    // Asocjacja z atrybutem między klasami Produkt i Klient, reprezentująca Zakup
    public class Purchase
    {
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int Quantity { get; set; }
        public DateTime PurchaseDate { get; set; }

        // Odniesienia do obiektów związanych z asocjacją
        public string ProductName { get; set; }  // Dodatkowe informacje o produkcie (bilecie)
        public decimal ProductPrice { get; set; }
    }
}