using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.FoodTypes.Commands.UpdateFoodType
{
    public class UpdateFoodTypeCommand :IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class UpdateFoodTypeCommandHandler : IRequestHandler<UpdateFoodTypeCommand, Response<int>>
    {
        private readonly IFoodTypeRepositoryAsync _FoodTypeRepositoryAsync;
        public UpdateFoodTypeCommandHandler(IFoodTypeRepositoryAsync FoodTypeRepositoryAsync)
        {
            _FoodTypeRepositoryAsync = FoodTypeRepositoryAsync;
        }

        public async Task<Response<int>> Handle(UpdateFoodTypeCommand request, CancellationToken cancellationToken)
        {
            var FoodType = await _FoodTypeRepositoryAsync.GetByIdAsync(request.Id);
            if(FoodType==null) { throw new EntityNotFoundException("Food Type", request.Id); }
            FoodType.Name = request.Name;
            await _FoodTypeRepositoryAsync.UpdateAsync(FoodType);
            return new Response<int>(FoodType.Id);
        }
    }
}
