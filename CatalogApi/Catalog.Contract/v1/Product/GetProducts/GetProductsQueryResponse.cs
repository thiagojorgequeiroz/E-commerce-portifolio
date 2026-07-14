namespace Catalog.Application.Contract.v1.Product.GetProducts
{
    public record GetProductsQueryResponse
    {
        public IEnumerable<GetProductsQueryResponseItems> Items { get; init; } = new List<GetProductsQueryResponseItems>();
        public int TotalPages { get; init; }
        public int TotalItems { get; init; }
        public bool NextPage { get; set; }
    }

    public record GetProductsQueryResponseItems
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public decimal Price { get; init; }
    }
}
