using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchConsulting.GestorInventario.Application.Dto;
using TouchConsulting.GestorInventario.Domain.Entities;

namespace TouchConsulting.GestorInventario.Application.Mappings
{
    public class AuthProfile : Profile
    {
        public AuthProfile() {

            // Mapear la entidad User a UserDto
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            // Mapear UserDto a AuthResponseDto (que hereda de UserDto)
            CreateMap<UserDto, AuthResponseDto>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());
        }
    }
}
