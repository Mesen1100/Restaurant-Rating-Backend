using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using CleanArchitecture.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Foods.Commands.UpdateFood
{
    public class UpdateFoodCommand:IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FoodImage { get; set; }
        public double Price { get; set; }
        public string ManagerUserId { get; set; }
    }
    public class UpdateFoodCommandHandler : IRequestHandler<UpdateFoodCommand, Response<int>>
    {
        private readonly IFoodRepositoryAsync _foodRepositoryAsync;


        public UpdateFoodCommandHandler(IFoodRepositoryAsync foodRepositoryAsync)
        {
            _foodRepositoryAsync = foodRepositoryAsync;

        }

        public async Task<Response<int>> Handle(UpdateFoodCommand request, CancellationToken cancellationToken)
        {
            var food =await _foodRepositoryAsync.GetByIdAsync(request.Id);
            if (food == null) { throw new EntityNotFoundException("Food", request.Id); }
            food.Name = request.Name;
            food.Description = request.Description;
            food.FoodImage = request.FoodImage;
            food.Price = request.Price;
            await _foodRepositoryAsync.UpdateAsync(food);
            return new Response<int>(food.Id);
        }
    }
}
