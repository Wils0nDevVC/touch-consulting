using Newtonsoft.Json;
using TouchConsulting.GestorInventario.Common.Interfaces;

namespace TouchConsulting.GestorInventario.Application.Dto
{
    public class ProductDto : IProduct
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Descripcion { get; set; }
        public required decimal Precio { get; set; }
        public required int CantidadInventario { get; set; }
        public int CategoryId { get; set; }
        //[JsonIgnore]
        //public CategoryDto? CategoryDto { get; set; }
    }
}
