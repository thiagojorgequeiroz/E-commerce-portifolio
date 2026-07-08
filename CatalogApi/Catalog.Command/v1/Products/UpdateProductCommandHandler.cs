using Catalog.Application.Contract.v1.Commun;
using Catalog.Application.Contract.v1.Product.UpdateProduct;
using Catalog.Domain.Respositories;
using MediatR;

namespace Catalog.Application.Command.v1.Products
{
    public class UpdateProductCommandHandler 
        (
            IProductsRespository productRepository,
            IUnitOfWork unitOfWork
        ) : IRequestHandler<UpdateProductCommand, CommunMessageResponse>
    {
        public async Task<CommunMessageResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);

            product.Update(
                request.Name ?? product.Name,
                request.Description ?? product.Description,
                request.Price ?? product.Price,
                request.IsActive ?? product.IsActive
            );

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new CommunMessageResponse ("Product updated successfully.");
        }
    }
}
