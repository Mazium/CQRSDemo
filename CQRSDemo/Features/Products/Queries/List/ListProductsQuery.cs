using CQRSDemo.Features.Product.Dtos;
using MediatR;

namespace CQRSDemo.Features.Product.Queries.List
{
    public  record ListProductsQuery : IRequest<List<ProductDto>>;

}
