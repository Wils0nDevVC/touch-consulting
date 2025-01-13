using AutoMapper;
using MediatR;
using TouchConsulting.GestorInventario.Common.Helpers;
using TouchConsulting.GestorInventario.Application.Dto;
using TouchConsulting.GestorInventario.Application.Interfaces;
using TouchConsulting.GestorInventario.Domain.Entities;
using FluentValidation;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TouchConsulting.GestorInventario.Application.Handlers.Commands.ProductController.Create
{
    public class Create : IRequest<Response<ProductDto>>
    {
        public ProductDto Request { get; }
        public Create(ProductDto request)
        {
            Request = request;
        }

        public class CreateHandler : IRequestHandler<Create, Response<ProductDto>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly IValidator<ProductDto> _validator;

            public CreateHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ProductDto> validator)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _validator = validator;
            }
            public async Task<Response<ProductDto>> Handle(Create request, CancellationToken cancellationToken)
            {
                var validationResult = await _validator.ValidateAsync(request.Request, cancellationToken);
                var response = new Response<ProductDto>();

                if (!validationResult.IsValid)
                {

                    response.Code = 400;
                    response.Message = "Errores de validació";
                    response.IsSuccess = false;
                    response.Data = null;
                    response.Errors = validationResult.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}").ToList();

                    return response;
   
                }



                var product = new Product()
                    {
                        Nombre = request.Request.Nombre,
                        Descripcion = request.Request.Descripcion,
                        Precio = request.Request.Precio,
                        CantidadInventario = request.Request.CantidadInventario,
                        CategoryId = request.Request.CategoryId
                    };


                    await _unitOfWork.Products.CreateAsync(product);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    var result = _mapper.Map<ProductDto>(product);

                    response.Code = 200;
                    response.Message = "Se registro correctamente el producto";
                    response.IsSuccess = true;
                    response.Data = result;
                    return response;



            }
        }
    }
}
