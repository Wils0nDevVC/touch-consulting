using MediatR;
using Microsoft.Extensions.Logging;
using TouchConsulting.GestorInventario.ExternalServices.Arroba.Models;
using TouchConsulting.GestorInventario.ExternalServices.Arroba.EndPoint;
using TouchConsulting.GestorInventario.Application.Dto;

namespace TouchConsulting.GestorInventario.Application.Handlers.Commands.MailController.SendMail
{ 
    public class SendMailBasic : IRequest<MailResponse>
    {
        public SendMailBasicRequestDto Model { get; }

        public SendMailBasic(SendMailBasicRequestDto model)
        {
            Model = model;
        }

        public class SendMailBasicHandler : IRequestHandler<SendMailBasic, MailResponse>
        {
            private readonly IMail _mail;
            private readonly ILogger<SendMailBasicHandler> _logger;

            public SendMailBasicHandler(IMail mail, ILogger<SendMailBasicHandler> logger)
            {
                _mail = mail;
                this._logger = logger;
            }

            public async Task<MailResponse> Handle(SendMailBasic request, CancellationToken cancellationToken)
            {

                var sendMailRequest = new MailRequest()
                {
                    AppOrigin = "TouchConsulting.GestorInventario",
                    MailFrom = "noreply@antamina.com",
                    MailFromAlias = request.Model.MailFromAlias ?? string.Empty,
                    MailSubject = request.Model.MailSubject ?? "BASEARCHITECTURE",
                    MailTo = request.Model.MailTo,
                    MailBodyHtml = request.Model.MailBodyHtml
                };

                var response = await _mail.SendEmail(sendMailRequest);
                return response.Result;

            }
        }
    }
}
