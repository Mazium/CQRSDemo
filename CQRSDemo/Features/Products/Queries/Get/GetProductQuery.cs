using CQRSDemo.Features.Product.Dtos;
using MediatR;

namespace CQRSDemo.Features.Product.Queries.Get
{
    public record GetProductQuery(Guid Id) : IRequest<ProductDto>;
   
}
