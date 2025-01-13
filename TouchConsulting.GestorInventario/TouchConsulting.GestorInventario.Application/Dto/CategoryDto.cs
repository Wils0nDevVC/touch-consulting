using Newtonsoft.Json;

namespace TouchConsulting.GestorInventario.Application.Dto
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        //[JsonIgnore]
        //public List<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
