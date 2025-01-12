using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchConsulting.GestorInventario.Common.Helpers;
using TouchConsulting.GestorInventario.Application.Dto;
using TouchConsulting.GestorInventario.Application.Interfaces;

namespace TouchConsulting.GestorInventario.Application.Handlers.Queries.ProductController.FindAll
{
    public class FindAll : IRequest<Response<IEnumerable<ProductDto>>>
    {
        public FindAll() { }

        public class FindAllHandler : IRequestHandler<FindAll, Response<IEnumerable<ProductDto>>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public FindAllHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<Response<IEnumerable<ProductDto>>> Handle(FindAll request, CancellationToken cancellationToken)
            {
                var response = new Response<IEnumerable<ProductDto>>();
                var query = _unitOfWork.Products.AsNoTracking();

                var entities = await query.ToListAsync(cancellationToken);
                var result = _mapper.Map<IEnumerable<ProductDto>>(entities);

                response.Code = 200;
                response.IsSuccess = true;
                response.Message = "Lista de productcos";
                response.Data = result;

                return response;
            }
        }
    }
}
