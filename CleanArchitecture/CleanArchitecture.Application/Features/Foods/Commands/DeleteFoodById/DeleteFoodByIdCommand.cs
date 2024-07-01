using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using CleanArchitecture.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Foods.Commands.DeleteFoodById
{
    public class DeleteFoodByIdCommand:IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string ManagerUserId { get; set; }
    }
    public class DeleteFoodByIdCommandHandler : IRequestHandler<DeleteFoodByIdCommand, Response<int>>
    {
        private readonly IFoodRepositoryAsync _foodRepositoryAsync;
        private readonly IMenuRepositoryAsync _menuRepositoryAsync;

        public DeleteFoodByIdCommandHandler(IFoodRepositoryAsync foodRepositoryAsync,IMenuRepositoryAsync menuRepositoryAsync)
        {
            _foodRepositoryAsync = foodRepositoryAsync;
            _menuRepositoryAsync = menuRepositoryAsync;
        }

        public async Task<Response<int>> Handle(DeleteFoodByIdCommand request, CancellationToken cancellationToken)
        {
            var food=await _foodRepositoryAsync.GetByIdAsync(request.Id);
            if (food == null) { throw new EntityNotFoundException("Food", request.Id); }
            bool CanManagerCreate = await _menuRepositoryAsync.CanManagerHandleFood(food.MenuId, request.ManagerUserId);
            if (!CanManagerCreate) { throw new UserNotHaveRoleTo("Delete"); }
            food.IsEnabled = false;
            await _foodRepositoryAsync.DeleteAsync(food);
            return new Response<int>(food.Id);
        }
    }
}
