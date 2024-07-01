using AutoMapper;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersQuery:IRequest<PagedResponse<IEnumerable<GetAllUsersViewModel>>>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

    }
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, PagedResponse<IEnumerable<GetAllUsersViewModel>>>
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IUserRepositoryAsync userRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllUsersViewModel>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var validfilter = _mapper.Map<GetAllUsersParameter>(request);
            var users =await _userRepository.GetPagedReponseAsync(validfilter.PageNumber,validfilter.PageSize);
            var userViewModel = _mapper.Map<IEnumerable<GetAllUsersViewModel>>(users);
            return new PagedResponse<IEnumerable<GetAllUsersViewModel>>(userViewModel,validfilter.PageSize,validfilter.PageNumber);
        }
    }
}
