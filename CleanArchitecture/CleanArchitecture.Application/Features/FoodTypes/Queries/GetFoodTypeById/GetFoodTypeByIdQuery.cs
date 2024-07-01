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

namespace CleanArchitecture.Core.Features.FoodTypes.Queries.GetFoodTypeById
{
    public class GetFoodTypeByIdQuery:IRequest<Response<FoodType>>
    {
        public int Id { get; set; }
    }
    public class GetFoodTypeByIdQueryHandler : IRequestHandler<GetFoodTypeByIdQuery, Response<FoodType>>
    {
        private readonly IFoodTypeRepositoryAsync _FoodTypeRepositoryAsync;

        public GetFoodTypeByIdQueryHandler(IFoodTypeRepositoryAsync FoodTypeRepositoryAsync)
        {
            _FoodTypeRepositoryAsync = FoodTypeRepositoryAsync;
        }

        public async Task<Response<FoodType>> Handle(GetFoodTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var FoodType = await _FoodTypeRepositoryAsync.GetByIdAsync(request.Id);
            if (FoodType == null) { throw new EntityNotFoundException("Food Type", request.Id); }
            return new Response<FoodType>(FoodType);
        }
    }
}
