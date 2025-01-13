using MediatR;
using Microsoft.Extensions.Logging;
using TouchConsulting.GestorInventario.ExternalServices.Arroba.Models;
using TouchConsulting.GestorInventario.ExternalServices.Arroba.EndPoint;
using TouchConsulting.GestorInventario.Application.Dto;
using TouchConsulting.GestorInventario.ExternalServices;

namespace TouchConsulting.GestorInventario.Application.Handlers.Commands.MailController.SendMail
{
    public class SendMailBasic : IRequest<ApiGenericResponse<MailResponse>>
    {
        public SendMailBasicRequestDto Model { get; }

        public SendMailBasic(SendMailBasicRequestDto model)
        {
            Model = model;
        }

        public class SendMailBasicHandler : IRequestHandler<SendMailBasic, ApiGenericResponse<MailResponse>>
        {
            private readonly IMail _mail;
            private readonly ILogger<SendMailBasicHandler> _logger;

            public SendMailBasicHandler(IMail mail, ILogger<SendMailBasicHandler> logger)
            {
                _mail = mail;
                this._logger = logger;
            }

            public async Task<ApiGenericResponse<MailResponse>> Handle(SendMailBasic request, CancellationToken cancellationToken)
            {
                var sendMailRequest = new MailRequest()
                {
                    AppOrigin = "TouchConsulting.GestorInventario",
                    MailFrom = "noreply@antamina.com",
                    MailFromAlias = request.Model.MailFromAlias ?? string.Empty,
                    MailSubject = request.Model.MailSubject ?? "TouchConsulting",
                    MailTo = request.Model.MailTo,
                    MailBodyHtml = request.Model.MailBodyHtml
                };

                // Enviar correo
                var response = await _mail.SendEmail(sendMailRequest);

                // Verificar el resultado de la respuesta del correo
                if (response != null && response.IsSuccess)
                {
                    _logger.LogInformation($"Correo enviado exitosamente a {request.Model.MailTo}");
                }
                else
                {
                    _logger.LogWarning($"Error al enviar correo a {request.Model.MailTo}: {response?.ErrorMessage ?? "No se pudo determinar el error"}");
                }

                return response;  // Devuelve la respuesta final que contiene el estado
            }
        }
    }
}
