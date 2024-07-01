using CleanArchitecture.Core.Features.Foods.Queries.GetAllFoods;
using CleanArchitecture.Core.Wrappers;
using CleanArchitecture.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Foods.Queries.GetFoodsByMenuId
{
    public class GetFoodByParameterQuery:IRequest<PagedResponse<IEnumerable<GetAllFoodsViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int FoodTypeId { get; set; }
        public int PlaceId { get; set; }
        public int MenuId { get; set; }
    }
    public class GetFoodByMenuIdQueryHandler:IRequestHandler<GetFoodByParameterQuery, PagedResponse<IEnumerable<GetAllFoodsViewModel>>>
    {
        private readonly IFoodRepositoryAsync _foodRepositoryAsync;

        public GetFoodByMenuIdQueryHandler(IFoodRepositoryAsync foodRepositoryAsync)
        {
            _foodRepositoryAsync = foodRepositoryAsync;
        }

        public async Task<PagedResponse<IEnumerable<GetAllFoodsViewModel>>> Handle(GetFoodByParameterQuery request, CancellationToken cancellationToken)
        {
            var validfilter = new GetFoodByParameterParameter
            {
                FoodTypeId = request.FoodTypeId,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                PlaceId = request.PlaceId,
                MenuId =request.MenuId,
            };
            return await _foodRepositoryAsync.GetFoodByParameter(validfilter); 
        }
    }
}
