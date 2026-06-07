using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Catalog.Application.Contract.v1.Product.CreateProduct
{
    public record CreateProductCommand : IRequest<Guid>
    {
        public required Guid Id { get; init; }

        public required string Name { get; init; }

        public required string Description { get; init; }

        public required decimal Price { get; init; }

        public required bool IsActive { get; init; }
    }
}
