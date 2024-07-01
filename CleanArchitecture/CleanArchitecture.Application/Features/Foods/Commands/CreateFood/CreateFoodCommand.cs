using CleanArchitecture.Core.Entities;
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

namespace CleanArchitecture.Core.Features.Foods.Commands.CreateFood
{
    public class CreateFoodCommand:IRequest<Response<int>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string FoodImage { get; set; }
        public double Price { get; set; }
        public int FoodTypeId { get; set; }
        public int MenuId { get; set; }
        public string ManagerUserId { get;set; } 
       
    }
    public class CreateFoodCommandHandler : IRequestHandler<CreateFoodCommand, Response<int>>
    {
        private readonly IFoodRepositoryAsync _foodRepositoryAsync;
        private readonly IMenuRepositoryAsync _menuRepositoryAsync;

        public CreateFoodCommandHandler(IFoodRepositoryAsync foodRepositoryAsync,IMenuRepositoryAsync menuRepositoryAsync)
        {
            _menuRepositoryAsync = menuRepositoryAsync;
            _foodRepositoryAsync = foodRepositoryAsync;
        }

        public async Task<Response<int>> Handle(CreateFoodCommand request, CancellationToken cancellationToken)
        {
            
            var food = new Food
            {
                Name = request.Name,
                Description = request.Description,
                FoodImage = request.FoodImage,
                Price = request.Price,
                CommentCount = 0,
                RatePoint = 0,
                RateCount = 0,
                FoodTypeId = request.FoodTypeId,
                MenuId = request.MenuId,
            };
            bool CanManagerCreate =await _menuRepositoryAsync.CanManagerHandleFood(food.MenuId, request.ManagerUserId);
            if (!CanManagerCreate) { throw new UserNotHaveRoleTo("Create"); }
            await _foodRepositoryAsync.AddAsync(food);
            return new Response<int>(food.Id);
        }
    }
}
