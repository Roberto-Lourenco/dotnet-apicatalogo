using APICatalogo.Context;
using APICatalogo.Entities;
using APICatalogo.WebAPI.Repositories;
using APICatalogo.WebAPI.Repositories.Interfaces;

namespace APICatalogo.Repositories;

internal class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ApiCatalogoContext context)
        : base(context)
    {
    }
}
