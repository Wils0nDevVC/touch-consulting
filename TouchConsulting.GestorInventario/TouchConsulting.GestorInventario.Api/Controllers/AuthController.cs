using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TouchConsulting.GestorInventario.Application.Dto;
using TouchConsulting.GestorInventario.Application.Handlers.Commands.AuthController.Login;
using TouchConsulting.GestorInventario.Application.Handlers.Commands.UserController.Create;
using static TouchConsulting.GestorInventario.Common.Helpers.ConstantWebApiController;

namespace TouchConsulting.GestorInventario.Api.Controllers
{
    [Route(AuthEndpoint.prefixAapi)]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(AuthEndpoint.CreateUser)]
        public async Task<IActionResult> Create(UserDto request)
        {
            var result = await _mediator.Send(new Create(request));
            return Ok(result);
        }

        [HttpPost(AuthEndpoint.Login)]
        public async Task<IActionResult> Login(AuthDto request)
        {
            var result = await _mediator.Send(new Login(request));
            return Ok(result);
        }

    }
}
