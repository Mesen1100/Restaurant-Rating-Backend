using AutoMapper;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.MenuTypes.Queries.GetAllMenuTypes
{
    public class GetAllMenuTypesQuery: IRequest<PagedResponse<IEnumerable<GetAllMenuTypesViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllMenuTypesQueryHandler : IRequestHandler<GetAllMenuTypesQuery, PagedResponse<IEnumerable<GetAllMenuTypesViewModel>>>
    {
        private readonly IMenuTypeRepositoryAsync _MenuTypeRepositoryAsync;
        private readonly IMapper _mapper;

        public GetAllMenuTypesQueryHandler(IMenuTypeRepositoryAsync MenuTypeRepositoryAsync,IMapper mapper)
        {
            _MenuTypeRepositoryAsync = MenuTypeRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllMenuTypesViewModel>>> Handle(GetAllMenuTypesQuery request, CancellationToken cancellationToken)
        {
            var validfilter = new GetAllMenuTypesParameter
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
            };
            var MenuType =await _MenuTypeRepositoryAsync.GetPagedReponseAsync(validfilter.PageNumber, validfilter.PageSize);
            var MenuTypeViewModel = _mapper.Map<IEnumerable<GetAllMenuTypesViewModel>>(MenuType);
            return new PagedResponse<IEnumerable<GetAllMenuTypesViewModel>>(MenuTypeViewModel, validfilter.PageNumber, validfilter.PageSize);
        }
    }
}
