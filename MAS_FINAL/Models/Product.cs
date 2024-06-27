namespace MAS_FINAL.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        // Metody dodawania i usuwania z koszyka
        public void AddToCart() 
        {
            // Implementacja dodawania do koszyka
        }

        public void RemoveFromCart()
        {
            // Implementacja usuwania z koszyka
        }
    }
}