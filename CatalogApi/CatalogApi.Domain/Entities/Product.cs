namespace Catalog.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Inventory Inventory { get; private set; }

        public Product(string name, string description, decimal price)
        {
            Id = Guid.CreateVersion7();
            Name = name;
            Description = description;
            Price = price;
            IsActive = true;
            Inventory = new Inventory(Id);
        }
    }
}
