using Asp.Versioning;
using Catalog.Application.Contract.v1.Commun;
using Catalog.Application.Contract.v1.Product.CreateProduct;
using Catalog.Application.Contract.v1.Product.GetProductById;
using Catalog.Application.Contract.v1.Product.GetProducts;
using Catalog.Application.Contract.v1.Product.UpdateInventory;
using Catalog.Application.Contract.v1.Product.UpdateProduct;
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

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CommunMessageResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(Guid id, UpdateProductCommand command)
        {
            command.Id = id;
            var retorno = await mediator.Send(command);
            return Ok(retorno);
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetProductsQueryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] GetProductsQueryRequest query)
        {
            var retorno = await mediator.Send(query);
            return Ok(retorno);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetProductByIdQueryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetProductByIdQueryRequest { Id = id };
            var retorno = await mediator.Send(query);
            return Ok(retorno);
        }

        [HttpPut("{id}/inventory")]
        [ProducesResponseType(typeof(CommunMessageResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateInventory(Guid id, UpdateInventoryCommand command)
        {
            command.Id = id;
            var retorno = await mediator.Send(command);
            return Ok(retorno);
        }
    }
}
