using CQRSDemo.Features.Product.Dtos;
using CQRSDemo.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRSDemo.Features.Product.Queries.List
{
    public class ListProductsQueryHandler(AppDbContext context) : IRequestHandler<ListProductsQuery, List<ProductDto>>
    {
        public async Task<List<ProductDto>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
        {
            return await context.Products
                .Select(p => new ProductDto(p.Id, p.Name, p.Description, p.Price))
                .ToListAsync();
        }
    }
}
