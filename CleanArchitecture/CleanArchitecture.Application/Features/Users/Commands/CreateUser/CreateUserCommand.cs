using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Enums;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand:IRequest<Response<string>>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<string>>
    {
        private readonly IUserRepositoryAsync _userRepository;
        public CreateUserCommandHandler(IUserRepositoryAsync userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Response<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.UserName,
                Email = request.Email,
                Role = request.Role,
            };
            await _userRepository.AddAsync(user);
            return new Response<string>(user.Id);
        }
    }
}
