using MediatR;

namespace CQRSDemo.Features.Product.Command.Create
{
    public record CreateProductCommand(string Name, string Description, decimal Price) : IRequest<Guid>;

}
