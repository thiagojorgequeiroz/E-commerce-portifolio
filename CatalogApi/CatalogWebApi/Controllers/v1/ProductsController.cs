using Asp.Versioning;
using Catalog.Application.Contract.v1.Product.CreateProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatalogWebApi.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class ProductsController(IMediator mediator) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand commad)
        {
            var retorno = await mediator.Send(commad);
            return Ok(retorno);
        }
    }
}
