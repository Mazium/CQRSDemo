using MediatR;

namespace CQRSDemo.Features.Products.Command.Update
{
    public record UpdateProductCommand(Guid Id, string Name, string Description, decimal Price) : IRequest<Unit>;

}
