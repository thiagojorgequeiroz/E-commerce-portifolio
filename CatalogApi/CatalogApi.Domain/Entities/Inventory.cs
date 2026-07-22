using Catalog.Domain.Exceptions;

namespace Catalog.Domain.Entities
{
    public class Inventory
    {
        public Guid ProductId { get; private set; }
        public int QuantityAvailable { get; private set; }
        public int QuantityReserved { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public Product Product { get; set; }

        public Inventory(Guid productId)
        {
            ProductId = productId;
            QuantityAvailable = 0;
            QuantityReserved = 0;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateQuantity(int quantity)
        {
            if (quantity < 0)
                throw new InvalidOperationException("Quantity cannot be negative.");
            QuantityAvailable = quantity;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddStock(int quantity)
        {
            QuantityAvailable += quantity;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ReserveStock(int quantity)
        {
            if (quantity <= 0)
                throw new BadRequestException("Quantity to reserve must be greater than zero.");

            if (quantity > QuantityAvailable)
                throw new BadRequestException("Insufficient stock.");

            QuantityAvailable -= quantity;
            QuantityReserved += quantity;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
