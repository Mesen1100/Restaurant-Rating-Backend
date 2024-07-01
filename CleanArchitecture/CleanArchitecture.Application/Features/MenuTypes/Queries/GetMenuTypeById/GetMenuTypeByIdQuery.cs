using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.MenuTypes.Queries.GetMenuTypeById
{
    public class GetMenuTypeByIdQuery:IRequest<Response<MenuType>>
    {
        public int Id { get; set; }
    }
    public class GetMenuTypeByIdQueryHandler : IRequestHandler<GetMenuTypeByIdQuery, Response<MenuType>>
    {
        private readonly IMenuTypeRepositoryAsync _MenuTypeRepositoryAsync;

        public GetMenuTypeByIdQueryHandler(IMenuTypeRepositoryAsync MenuTypeRepositoryAsync)
        {
            _MenuTypeRepositoryAsync = MenuTypeRepositoryAsync;
        }

        public async Task<Response<MenuType>> Handle(GetMenuTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var MenuType = await _MenuTypeRepositoryAsync.GetByIdAsync(request.Id);
            if (MenuType == null) { throw new EntityNotFoundException("Menu Type", request.Id); }
            return new Response<MenuType>(MenuType);
        }
    }
}
