using CleanArchitecture.Core.Entities;
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

namespace CleanArchitecture.Core.Features.FoodRates.Commands.CreateFoodRate
{
    public class CreateFoodRateCommand:IRequest<Response<int>>
    {
        public double PriceRate { get; set; }
        public double TasteRate { get; set; }
        public int FoodId { get; set; }
        public string UserId { get; set; }
    }
    public class CreateFoodRateCommandHandler : IRequestHandler<CreateFoodRateCommand, Response<int>>
    {
        private readonly IFoodRateRepositoryAsync _foodRateRepositoryAsync;
        private readonly IFoodRepositoryAsync _foodRepositoryAsync;
        private readonly IMenuRepositoryAsync _menuRepositoryAsync;

        public CreateFoodRateCommandHandler(IFoodRateRepositoryAsync foodRateRepositoryAsync, IFoodRepositoryAsync foodRepositoryAsync,IMenuRepositoryAsync menuRepositoryAsync)
        {
            _foodRateRepositoryAsync = foodRateRepositoryAsync;
            _foodRepositoryAsync = foodRepositoryAsync;
            _menuRepositoryAsync = menuRepositoryAsync;
        }

        public async Task<Response<int>> Handle(CreateFoodRateCommand request, CancellationToken cancellationToken)
        {
            var food=_foodRepositoryAsync.GetFoodById(request.FoodId);
            if (food == null) { throw new EntityNotFoundException("Food", request.FoodId); }
            var foodRate = new FoodRate
            {
                PriceRate = request.PriceRate,
                TasteRate = request.TasteRate,
                FoodId = request.FoodId,
                UserId = request.UserId,
                RatePoint = (request.PriceRate + request.TasteRate) / 2,
                IsEnabled = true,
                IsShowned = true,
            };
            await _foodRateRepositoryAsync.AddAsync(foodRate);
            Food newFood = _foodRepositoryAsync.RateUpdateAsync(foodRate);
            await _foodRepositoryAsync.UpdateAsync(newFood);
            Menu newMenu = _menuRepositoryAsync.RateUpdateAsync(foodRate);
            await _menuRepositoryAsync.UpdateAsync(newMenu);
            return new Response<int>(foodRate.Id);
        }
    }
}
