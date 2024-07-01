using AutoMapper;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.FoodTypes.Queries.GetAllFoodTypes
{
    public class GetAllFoodTypesQuery: IRequest<PagedResponse<IEnumerable<GetAllFoodTypesViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllFoodTypesQueryHandler : IRequestHandler<GetAllFoodTypesQuery, PagedResponse<IEnumerable<GetAllFoodTypesViewModel>>>
    {
        private readonly IFoodTypeRepositoryAsync _FoodTypeRepositoryAsync;
        private readonly IMapper _mapper;

        public GetAllFoodTypesQueryHandler(IFoodTypeRepositoryAsync FoodTypeRepositoryAsync,IMapper mapper)
        {
            _FoodTypeRepositoryAsync = FoodTypeRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllFoodTypesViewModel>>> Handle(GetAllFoodTypesQuery request, CancellationToken cancellationToken)
        {
            var validfilter = new GetAllFoodTypesParameter
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
            };
            var FoodType =await _FoodTypeRepositoryAsync.GetPagedReponseAsync(validfilter.PageNumber, validfilter.PageSize);
            var FoodTypeViewModel = _mapper.Map<IEnumerable<GetAllFoodTypesViewModel>>(FoodType);
            return new PagedResponse<IEnumerable<GetAllFoodTypesViewModel>>(FoodTypeViewModel, validfilter.PageNumber, validfilter.PageSize);
        }
    }
}
