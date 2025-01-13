using Microsoft.AspNetCore.Mvc;
using MediatR;
using static TouchConsulting.GestorInventario.Common.Helpers.ConstantWebApiController;
using TouchConsulting.GestorInventario.Application.Handlers.Queries.ProductController.FindAll;
using Reec.Inspection;
using TouchConsulting.GestorInventario.Application.Dto;
using TouchConsulting.GestorInventario.Application.Handlers.Commands.ProductController.Create;
using Microsoft.AspNetCore.Authorization;
using TouchConsulting.GestorInventario.Application.Handlers.Commands.ProductController.Delete;
using TouchConsulting.GestorInventario.Application.Handlers.Commands.ProductController.Update;

namespace TouchConsulting.GestorInventario.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route(ProductEndpoint.prefixAapi)]

    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

      

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("FindAll")]
        public async Task<IActionResult> FindAll()
        {
            var result = await _mediator.Send(new FindAll());

            if (result == null)
                throw new ReecException(ReecEnums.Category.PartialContent, "No se encontraron registros.");

            return Ok(result);          
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(ProductDto request)
        {
            var result = await _mediator.Send(new Create(request));
            return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(ProductDto request)
        {
            var result = await _mediator.Send(new Delete(request));
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(ProductDto request)
        {
            var result = await _mediator.Send(new Update(request));
            return Ok(result);
        }
    }
}
