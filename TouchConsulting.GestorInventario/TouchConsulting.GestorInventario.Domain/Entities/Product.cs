using System;
using System.Collections.Generic;
using System.Text;

namespace TouchConsulting.GestorInventario.Domain.Entities
{
    public class Product : BaseEntity
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Descripcion { get; set; }
        public required decimal Precio { get; set; }
        public required int CantidadInventario { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
