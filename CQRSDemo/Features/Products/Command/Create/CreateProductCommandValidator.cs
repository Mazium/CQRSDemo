using CQRSDemo.Features.Product.Command.Create;
using CQRSDemo.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CQRSDemo.Features.Products.Command.Create
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        private readonly AppDbContext _context;

        public CreateProductCommandValidator(AppDbContext context)
        {
            _context = context;

            RuleFor(p => p.Name)
                .NotEmpty()
                .MinimumLength(4)
                .MustAsync(BeUniqueName).WithMessage("The specified product name already exists.");

            RuleFor(p => p.Price)
                .GreaterThan(0);
        }

        private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            return !await _context.Products.AnyAsync(p => p.Name == name, cancellationToken);
        }
    }
}
