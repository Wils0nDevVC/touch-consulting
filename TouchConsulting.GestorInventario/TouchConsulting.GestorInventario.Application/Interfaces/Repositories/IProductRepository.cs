using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchConsulting.GestorInventario.Domain.Entities;

namespace TouchConsulting.GestorInventario.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> FindByProductsAsync(int cantidad);
    }
}
