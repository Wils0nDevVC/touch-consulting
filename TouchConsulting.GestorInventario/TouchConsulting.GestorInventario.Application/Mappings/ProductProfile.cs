using AutoMapper;
using TouchConsulting.GestorInventario.Application.Dto;
using TouchConsulting.GestorInventario.Domain.Entities;

namespace TouchConsulting.GestorInventario.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile() {
            CreateMap<ProductDto, Product>();
        }
    }
}
