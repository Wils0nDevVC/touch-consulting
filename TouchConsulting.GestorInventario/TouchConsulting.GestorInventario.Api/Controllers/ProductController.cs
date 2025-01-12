using Microsoft.AspNetCore.Mvc;
using MediatR;
using static TouchConsulting.GestorInventario.Common.Helpers.ConstantWebApiController;
using TouchConsulting.GestorInventario.Application.Handlers.Queries.ProductController.FindAll;
using Reec.Inspection;

namespace TouchConsulting.GestorInventario.Api.Controllers
{
    [Route(ProductEndpoint.prefixAapi)]
    [ApiController]
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
    }
}
