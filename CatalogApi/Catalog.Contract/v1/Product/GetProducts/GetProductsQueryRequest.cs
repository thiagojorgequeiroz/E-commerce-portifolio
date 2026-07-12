using Catalog.Application.Contract.v1.Product.CreateProduct;
using FluentValidation;
using MediatR;

namespace Catalog.Application.Contract.v1.Product.GetProducts
{
    public record GetProductsQueryRequest : IRequest<GetProductsQueryResponse>
    {
        public int? Page { get; init; }
        public int? PageSize { get; init; }
    }

    public class GetProductsQueryRequestValidator : AbstractValidator<GetProductsQueryRequest>
    {
        public GetProductsQueryRequestValidator()
        {
            RuleFor(x => x.Page).NotNull().GreaterThan(0).WithMessage("The page is requierd.");
            RuleFor(x => x.PageSize).NotNull().GreaterThan(0).WithMessage("The page size is required.");
        }
    }
}
