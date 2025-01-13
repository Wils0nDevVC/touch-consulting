using Microsoft.EntityFrameworkCore;
using TouchConsulting.GestorInventario.Application.Dto;
using TouchConsulting.GestorInventario.Application.Interfaces.Repositories;
using TouchConsulting.GestorInventario.Domain.Entities;
using TouchConsulting.GestorInventario.Infrastructure.Persitence;



namespace TouchConsulting.GestorInventario.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly BaseDbContext _context;

        public ProductRepository(BaseDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> FindByProductsAsync(int cantidad)
        {
            var products = await _context.Product
                                          .Where(p => p.CantidadInventario < 5)
                                          .ToListAsync();

            return products;
        }
    }
}
