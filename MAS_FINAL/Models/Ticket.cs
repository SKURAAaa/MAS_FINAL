using System;

namespace MAS_FINAL.Models
{
    public abstract class Ticket
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public abstract string TicketType { get; }
        public abstract decimal Price { get; }

        public abstract void UpgradeTicket();
    }

    public class NormalTicket : Ticket
    {
        public override string TicketType => "Normalny";
        public override decimal Price => 50;

        public override void UpgradeTicket()
        {
            // Logika aktualizacji biletu
        }
    }

    public class SpecialTicket : Ticket
    {
        public override string TicketType => "Specjalny";
        public override decimal Price => 100;

        public override void UpgradeTicket()
        {
            // Logika aktualizacji biletu
        }
    }
}