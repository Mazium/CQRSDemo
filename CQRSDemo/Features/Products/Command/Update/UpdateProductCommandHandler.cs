using CQRSDemo.Persistence;
using MediatR;

namespace CQRSDemo.Features.Products.Command.Update
{
    public class UpdateProductCommandHandler(AppDbContext context) : IRequestHandler<UpdateProductCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await context.Products.FindAsync(command.Id);
            if (product == null) return Unit.Value;

            product.Name = command.Name;
            product.Description = command.Description;
            product.Price = command.Price;

            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }

}
