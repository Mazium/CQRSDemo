using CQRSDemo.Features.Product.Command.Create;
using CQRSDemo.Features.Product.Queries.Get;
using CQRSDemo.Features.Product.Queries.List;
using CQRSDemo.Features.Products.Command.Delete;
using CQRSDemo.Features.Products.Command.Update;
using CQRSDemo.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRSDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMediator mediatr;

        public ProductController(ISender mediator, IMediator mediatr)
        {
            _mediator = mediator;
            this.mediatr = mediatr;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var product = await _mediator.Send(new GetProductQuery(id));
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> ListProducts()
        {
            var products = await _mediator.Send(new ListProductsQuery());
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            var productId = await _mediator.Send(command);
            if (Guid.Empty == productId)
                return BadRequest();
            await mediatr.Publish(new ProductCreatedNotification(productId));
            return CreatedAtAction(nameof(GetProduct), new { id = productId }, new { id = productId });
        }

         [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductCommand command)
        {
            if (id != command.Id)
                return BadRequest("The ID in the route does not match the ID in the command.");

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _mediator.Send(new DeleteProductCommand(id));
            return NoContent();
        }
    }
}

