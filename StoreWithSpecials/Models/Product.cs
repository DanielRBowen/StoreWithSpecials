namespace StoreWithSpecials.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public string Tier { get; set; }

        public decimal Price { get; set; }
    }
}
