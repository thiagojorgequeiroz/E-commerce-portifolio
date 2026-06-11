using Catalog.Application.Contract.v1.Product.CreateProduct;
using Catalog.Domain.Entities;
using Catalog.Domain.Respositories;
using MediatR;

namespace Catalog.Application.Command.v1.Products
{
    public class CreateProductCommandHandler
        (
            IProductsRespository productsRespository,
            IUnitOfWork unitOfWork
        ) : IRequestHandler<CreateProductCommand, Guid>
    {
        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(request.Name!, request.Description!, request.Price!.Value);
            await productsRespository.CreateAsync(product);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return product.Id;
        }
    }
}
