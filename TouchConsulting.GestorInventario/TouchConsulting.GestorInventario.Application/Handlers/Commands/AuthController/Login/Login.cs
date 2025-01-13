using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchConsulting.GestorInventario.Common.Helpers;
using TouchConsulting.GestorInventario.Application.Dto;
using Microsoft.Extensions.Configuration;
using TouchConsulting.GestorInventario.Domain.Entities;
using TouchConsulting.GestorInventario.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Text.Json.Serialization;
using System.Text.Json;
using TouchConsulting.GestorInventario.Application.Interfaces.Repositories;

namespace TouchConsulting.GestorInventario.Application.Handlers.Commands.AuthController.Login
{
    public class Login : IRequest<Response<AuthResponseDto>>
    {
        public AuthDto Request { get; }

        public Login(AuthDto request)
        {
            Request = request;
        }

        public class LoginHandler : IRequestHandler<Login, Response<AuthResponseDto>>
        {
            private readonly IConfiguration _configuration;
            private readonly IAuthService _authService;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;


            public LoginHandler( IConfiguration configuration, IAuthService authService, IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper)
            {
                _configuration = configuration;
                _authService = authService;
                _unitOfWork = unitOfWork;
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<Response<AuthResponseDto>> Handle(Login request, CancellationToken cancellationToken)
            {

                var response = new Response<AuthResponseDto>();
                try
                {

                    var usuario = await _userRepository.GetUserAuth(request.Request.Email, _authService.EncriptarSHA256(request.Request.Password));

                    if (usuario != null)
                    {

                        var token = _authService.GenerarJWT(usuario);
                        var userDto = _mapper.Map<UserDto>(usuario);
                        var userAuth = _mapper.Map<AuthResponseDto>(userDto);

                        // Asignar el token generado
                        userAuth.Token = token;

                        response.Code = 200;
                        response.Errors = null;
                        response.IsSuccess = true;
                        response.Message = "Datos correctos";
                        response.Data = userAuth;

                        return response;
                    }

                    response.Code = 404;
                    response.Errors = null;
                    response.IsSuccess = false;
                    response.Message = "Datos Incorrectos";
                    response.Data = null;
                    return response;
                }
                catch (Exception ex)
                {

                    response.Code = 500;
                    response.Errors = null;
                    response.IsSuccess = false;
                    response.Message = ex.Message;
                    response.Data = null;
                    return response;
                }
            }
        }

    }
}
