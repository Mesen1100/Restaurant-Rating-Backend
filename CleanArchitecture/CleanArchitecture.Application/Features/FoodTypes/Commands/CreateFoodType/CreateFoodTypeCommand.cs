using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.FoodTypes.Commands.CreateFoodType
{
    public class CreateFoodTypeCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
    }
    public class CreateFoodTypeCommandHandler : IRequestHandler<CreateFoodTypeCommand,Response<int>>
    {
        private readonly IFoodTypeRepositoryAsync _FoodtypeRepository;
        public CreateFoodTypeCommandHandler(IFoodTypeRepositoryAsync FoodtypeRepository)
        {
            _FoodtypeRepository = FoodtypeRepository;
        }

        public async Task<Response<int>> Handle(CreateFoodTypeCommand request, CancellationToken cancellationToken)
        {
            var FoodType = new FoodType
            {
                Name = request.Name,
            };
            await _FoodtypeRepository.AddAsync(FoodType);
            return new Response<int>(FoodType.Id);
        }
    }
}
