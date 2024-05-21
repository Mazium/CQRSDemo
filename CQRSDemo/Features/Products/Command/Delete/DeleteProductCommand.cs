using MediatR;

namespace CQRSDemo.Features.Products.Command.Delete
{
    public record DeleteProductCommand(Guid Id) : IRequest;
}
