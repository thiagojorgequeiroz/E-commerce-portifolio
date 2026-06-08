namespace Catalog.Domain.Models.Product
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public Product(string name, string description, decimal price, bool isActive)
        {
            Id = Guid.CreateVersion7();
            Name = name;
            Description = description;
            Price = price;
            IsActive = isActive;
        }
    }
}
