using System.Collections.Generic;

namespace MAS_FINAL.Models
{
    public class Customer : Person
    {
        public Ticket Ticket { get; set; }
        public List<Purchase> Purchases { get; set; }

        public Customer()
        {
            Purchases = new List<Purchase>();
        }

        // Metoda do zakupu biletu
        public void BuyTicket(Ticket ticket)
        {
            this.Ticket = ticket;
        }

        // Metoda do aktualizacji biletu
        public void UpgradeTicket()
        {
            if (this.Ticket is NormalTicket)
            {
                this.Ticket = new SpecialTicket
                {
                    Id = this.Ticket.Id,
                    PurchaseDate = this.Ticket.PurchaseDate
                };
            }
        }
    }
}