using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchConsulting.GestorInventario.Domain.Entities;

namespace TouchConsulting.GestorInventario.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<int> CreateStoredProcedure(Product productEntity);
    }
}
