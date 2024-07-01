using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.MenuTypes.Commands.UpdateMenuType
{
    public class UpdateMenuTypeCommand :IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class UpdateMenuTypeCommandHandler : IRequestHandler<UpdateMenuTypeCommand, Response<int>>
    {
        private readonly IMenuTypeRepositoryAsync _MenuTypeRepositoryAsync;
        public UpdateMenuTypeCommandHandler(IMenuTypeRepositoryAsync MenuTypeRepositoryAsync)
        {
            _MenuTypeRepositoryAsync = MenuTypeRepositoryAsync;
        }

        public async Task<Response<int>> Handle(UpdateMenuTypeCommand request, CancellationToken cancellationToken)
        {
            var MenuType = await _MenuTypeRepositoryAsync.GetByIdAsync(request.Id);
            if(MenuType==null) { throw new EntityNotFoundException("Menu Type", request.Id); }
            MenuType.Name = request.Name;
            await _MenuTypeRepositoryAsync.UpdateAsync(MenuType);
            return new Response<int>(MenuType.Id);
        }
    }
}
