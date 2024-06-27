using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MAS_FINAL.Models;
using MAS_FINAL.Services;

namespace MAS_FINAL
{
    public partial class TicketPurchaseWindow : Window
    {
        private ZooManagementSystem zooManagementSystem;
        private Customer currentCustomer;

        public event EventHandler TicketPurchased;

        public TicketPurchaseWindow(ZooManagementSystem system, Customer customer)
        {
            InitializeComponent();
            zooManagementSystem = system;
            currentCustomer = customer;
        }

        private void PurchaseTicket_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TicketTypeComboBox == null || TicketTypeComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Proszę wybrać rodzaj biletu.");
                    return;
                }

                if (string.IsNullOrEmpty(TicketQuantityTextBox.Text) ||
                    !int.TryParse(TicketQuantityTextBox.Text, out int quantity))
                {
                    MessageBox.Show("Proszę wprowadzić poprawną liczbę biletów.");
                    return;
                }

                var selectedTicketType = (TicketTypeComboBox.SelectedItem as ComboBoxItem).Content.ToString();

                Ticket ticket = null;
                if (selectedTicketType == "Normalny")
                {
                    ticket = new NormalTicket
                    {
                        Id = zooManagementSystem.GenerateTicketId(),
                        PurchaseDate = DateTime.Now
                    };
                }
                else if (selectedTicketType == "Specjalny")
                {
                    ticket = new SpecialTicket
                    {
                        Id = zooManagementSystem.GenerateTicketId(),
                        PurchaseDate = DateTime.Now
                    };
                }

                if (ticket != null)
                {
                    if (currentCustomer == null)
                    {
                        currentCustomer = new Customer
                        {
                            Id = zooManagementSystem.GenerateCustomerId(),
                            FirstName = "Guest",
                            LastName = "User",
                            Ticket = ticket
                        };
                        zooManagementSystem.AddCustomer(currentCustomer);
                    }
                    else
                    {
                        currentCustomer.BuyTicket(ticket);
                    }

                    // Dodanie informacji o zakupie
                    var purchase = new Purchase
                    {
                        ProductId = ticket.Id,
                        CustomerId = currentCustomer.Id,
                        Quantity = quantity,
                        PurchaseDate = DateTime.Now,
                        ProductName = ticket.TicketType,
                        ProductPrice = ticket.Price
                    };
                    
                    currentCustomer.Purchases.Add(purchase);
                    
                    zooManagementSystem.UpdateCustomer(currentCustomer);
                    zooManagementSystem.SaveCustomersToFile("customers.json");
                    MessageBox.Show("Bilet został zakupiony.");

                    TicketPurchased?.Invoke(this, EventArgs.Empty);

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}");
            }
        }

        private void UpgradeTicket_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currentCustomer.Ticket == null || currentCustomer.Ticket.TicketType == "Specjalny")
                {
                    MessageBox.Show("Nie można zaktualizować biletu.");
                    return;
                }

                currentCustomer.UpgradeTicket();
                zooManagementSystem.UpdateCustomer(currentCustomer);
                zooManagementSystem.SaveCustomersToFile("customers.json");
                MessageBox.Show("Bilet został zaktualizowany.");

                TicketPurchased?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}");
            }
        }
    }
}