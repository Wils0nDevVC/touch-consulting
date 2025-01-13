using MediatR;
using Microsoft.Extensions.Logging;
using TouchConsulting.GestorInventario.Application.Dto;
using TouchConsulting.GestorInventario.Application.Interfaces;
using TouchConsulting.GestorInventario.Domain.Entities;
using System.Linq;
using TouchConsulting.GestorInventario.Common.Helpers;
using TouchConsulting.GestorInventario.Application.Handlers.Commands.MailController.SendMail;
using TouchConsulting.GestorInventario.Application.Interfaces.Repositories;
using TouchConsulting.GestorInventario.Common.Utils;
using TouchConsulting.GestorInventario.Common.Interfaces;

namespace TouchConsulting.GestorInventario.Application.Handlers.Commands.ProductController.CheckInventoryAndSendEmail
{
    public class SendEmailIfLowStock : IRequest<Response<string>>
    {
        public ProductDto Request { get; }

        public SendEmailIfLowStock(ProductDto request)
        {
            Request = request;
        }
        public class SendEmailIfLowStockHandler : IRequestHandler<SendEmailIfLowStock, Response<string>>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMediator _mediator;
            private readonly ILogger<SendEmailIfLowStockHandler> _logger;

            public SendEmailIfLowStockHandler(IProductRepository productRepository, IMediator mediator, ILogger<SendEmailIfLowStockHandler> logger)
            {
                _productRepository = productRepository;
                _mediator = mediator;
                _logger = logger;
            }

            public async Task<Response<string>> Handle(SendEmailIfLowStock request, CancellationToken cancellationToken)
            {
                var response = new Response<string>();

                try
                {
                    // Obtener productos con stock menor a 5
                    var lowStockProducts = await _productRepository.FindByProductsAsync(request.Request.CantidadInventario);

                    if (!lowStockProducts.Any())
                    {
                        response.Code = 200;
                        response.Message = "No hay productos con bajo inventario.";
                        response.IsSuccess = true;
                        return response;
                    }

                    var productDtos = lowStockProducts.Select(p => new ProductDto
                    {
                        Id = p.Id,
                        Nombre = p.Nombre,
                        Descripcion = p.Descripcion,
                        Precio = p.Precio,
                        CantidadInventario = p.CantidadInventario,
                        CategoryId = p.CategoryId
                    }).Cast<IProduct>().ToList();

                    var mailBody = BodyEmail.BuildMai(productDtos);

                    // Enviar el correo
                    var sendMailRequest = new SendMailBasicRequestDto
                    {
                        MailTo = "admin@company.com",
                        MailSubject = "Alerta: Bajo Inventario",
                        MailBodyHtml = mailBody
                    };

                    var mailResponse = await _mediator.Send(new SendMailBasic(sendMailRequest));



                    if (mailResponse.IsSuccess)
                    {
                        response.Code = 200;
                        response.Message = "Correo enviado exitosamente.";
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.Code = 500;
                        response.Message = "Error al enviar el correo.";
                        response.IsSuccess = false;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al procesar la solicitud de bajo inventario.");
                    response.Code = 500;
                    response.Message = "Ocurrió un error inesperado.";
                    response.IsSuccess = false;
                }

                return response;
            }
        }
    }
}
