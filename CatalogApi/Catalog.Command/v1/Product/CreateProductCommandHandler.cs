using Catalog.Application.Contract.v1.Product.CreateProduct;
using MediatR;

namespace Catalog.Application.Command.v1.Product
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        public Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Guid.NewGuid());
        }
    }
}
