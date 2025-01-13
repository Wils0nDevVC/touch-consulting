using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchConsulting.GestorInventario.Common.Helpers;
using TouchConsulting.GestorInventario.Application.Dto;
using AutoMapper;
using FluentValidation;
using TouchConsulting.GestorInventario.Application.Interfaces;
using TouchConsulting.GestorInventario.Domain.Entities;
using System.Text.Json;
using TouchConsulting.GestorInventario.Application.Interfaces.Repository;

namespace TouchConsulting.GestorInventario.Application.Handlers.Commands.UserController.Create
{
    public class Create : IRequest<Response<UserDto>>
    {

        public UserDto Request { get; }
        public Create(UserDto request)
        {
            Request = request;
        }

        public class CreateHandler : IRequestHandler<Create, Response<UserDto>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly IValidator<UserDto> _validator;
            private readonly IUserRepository _userRepository;
            private readonly IAuthService _authService;

            public CreateHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UserDto> validator, IAuthService authService, IUserRepository userRepository)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _validator = validator;
                _authService = authService;
                _userRepository = userRepository;   
            }
            public async Task<Response<UserDto>> Handle(Create request, CancellationToken cancellationToken)
            {
                var response = new Response<UserDto>();

                try
                {
                    var validationResult = await _validator.ValidateAsync(request.Request, cancellationToken);

                    if (!validationResult.IsValid)
                    {

                        response.Code = 400;
                        response.Message = "Errores de validació";
                        response.IsSuccess = false;
                        response.Data = null;
                        response.Errors = validationResult.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}").ToList();

                        return response;

                    }

                    var existingUser = await _userRepository.FindByEmailAsync(request.Request.Email);



                    if (existingUser != null)
                    {
                        response.Code = 400;
                        response.Message = "El correo electrónico ya está en uso.";
                        response.IsSuccess = false;
                        response.Data = null;
                        return response;
                    }

                    var user = new User()
                    {
                        Name = request.Request.Name,
                        LastName = request.Request.LastName,
                        Email = request.Request.Email,
                        Password = _authService.EncriptarSHA256(request.Request.Password),
                        UserRoles = request.Request.UserRoles.Select(ur => new UserRole
                        {
                            RoleId = ur.RoleId,
                            AssignedDate = ur.AssignedDate
                        }).ToList()
                    };


                    await _unitOfWork.Users.CreateAsync(user);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);



                    response.Code = 200;
                    response.Message = "Se registro correctamente el usuario";
                    response.IsSuccess = true;
                    response.Data = null;
                    return response;
                }
                catch (Exception ex)
                {
                    response.Code = 500;
                    response.Message = ex.Message;
                    response.IsSuccess = false;
                    response.Data = null;
                    return response;
                }



            }
        }

    }
}
