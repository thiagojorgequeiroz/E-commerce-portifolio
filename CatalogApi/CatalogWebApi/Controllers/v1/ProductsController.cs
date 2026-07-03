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
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CreateProductCommand commad)
        {
            var retorno = await mediator.Send(commad);
            return CreatedAtAction(nameof(Create), new { id = retorno }, retorno);
        }
    }
}
