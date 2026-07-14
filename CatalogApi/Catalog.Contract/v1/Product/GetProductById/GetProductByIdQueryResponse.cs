namespace Catalog.Application.Contract.v1.Product.GetProductById
{
    public record GetProductByIdQueryResponse
    {
        public Guid Id { get; init; }
        public string? Name { get; init; }
        public string? Description { get; init; }
        public decimal Price { get; init; }
        public int AvailableQuantity { get; init; }
    }
}
