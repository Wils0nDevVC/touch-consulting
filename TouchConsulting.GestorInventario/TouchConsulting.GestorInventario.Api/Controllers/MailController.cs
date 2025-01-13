using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TouchConsulting.GestorInventario.Application.Dto;
using TouchConsulting.GestorInventario.Application.Handlers.Commands.MailController.SendMail;
using static TouchConsulting.GestorInventario.Common.Helpers.ConstantWebApiController;

namespace TouchConsulting.GestorInventario.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route(MailEndpoint.prefixAapi)]
    public class MailController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MailController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost("SendMail")]
        public async Task<IActionResult> SendMailBasic(SendMailBasicRequestDto model)
        {
            var result = await _mediator.Send(new SendMailBasic(model));
            return Ok(result);
        }
    }
}
