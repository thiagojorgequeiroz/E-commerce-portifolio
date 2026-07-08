using Catalog.Domain.Exceptions;

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

        #region Constructors
        public Product(string name, string description, decimal price)
        {
            ValidateName(name);
            ValidateDescription(description);
            ValidatePrice(price);

            Id = Guid.CreateVersion7();
            Name = name;
            Description = description;
            Price = price;
            IsActive = true;
            Inventory = new Inventory(Id);
        }
        #endregion

        public void Update(string name, string description, decimal price, bool isActive)
        {
            ValidateName(name);
            ValidateDescription(description);
            ValidatePrice(price);
            Name = name;
            Description = description;
            Price = price;
            IsActive = isActive;
        }

        #region Validations
        private void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"Product name cannot be empty. Value: {name}");
        }

        private void ValidateDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new DomainException($"Product description cannot be empty. Value: {description}");
        }

        private void ValidatePrice(decimal price)
        {
            if (price <= 0)
                throw new ArgumentException($"Product price must be greater than zero. Value: {price}");
        }
        #endregion
    }
}
