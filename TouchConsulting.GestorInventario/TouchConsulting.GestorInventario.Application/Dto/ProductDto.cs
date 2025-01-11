using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchConsulting.GestorInventario.Domain.Entities;

namespace TouchConsulting.GestorInventario.Application.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Descripcion { get; set; }
        public required decimal Precio { get; set; }
        public required int CantidadInventario { get; set; }
        public int CategoryId { get; set; }
        public CategoryDto CategoryDto { get; set; }
    }
}
