using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace Catalog.Application.Contract.v1.Product.GetProductById
{
    public record GetProductByIdQueryRequest : IRequest<GetProductByIdQueryResponse>
    {
        [JsonIgnore]
        [BindingBehavior(BindingBehavior.Never)]
        public Guid Id { get; set; }
    }
}
