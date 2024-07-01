using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.FoodTypes.Commands.DeleteFoodTypeById
{
    public class DeleteFoodTypeByIdCommand :IRequest<Response<int>>
    {
        public int Id { get; set; }
    }
    public class DeleteFoodTypeByIdCommandHandler : IRequestHandler<DeleteFoodTypeByIdCommand, Response<int>>
    {
        private readonly IFoodTypeRepositoryAsync _FoodTypeRepositoryAsync;

        public DeleteFoodTypeByIdCommandHandler(IFoodTypeRepositoryAsync FoodTypeRepositoryAsync)
        {
            _FoodTypeRepositoryAsync = FoodTypeRepositoryAsync;
        }

        public async Task<Response<int>> Handle(DeleteFoodTypeByIdCommand request, CancellationToken cancellationToken)
        {
            var FoodType = await _FoodTypeRepositoryAsync.GetByIdAsync(request.Id);
            if (FoodType == null) { throw new EntityNotFoundException("Food Type",request.Id); }
            await _FoodTypeRepositoryAsync.DeleteAsync(FoodType);
            return new Response<int>(FoodType.Id);

        }
    }
}
