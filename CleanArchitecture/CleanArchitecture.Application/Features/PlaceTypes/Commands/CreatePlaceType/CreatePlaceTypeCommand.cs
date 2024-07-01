using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.PlaceTypes.Commands.CreatePlaceType
{
    public class CreatePlaceTypeCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
    }
    public class CreatePlaceTypeCommandHandler : IRequestHandler<CreatePlaceTypeCommand,Response<int>>
    {
        private readonly IPlaceTypeRepositoryAsync _placetypeRepository;
        public CreatePlaceTypeCommandHandler(IPlaceTypeRepositoryAsync placetypeRepository)
        {
            _placetypeRepository = placetypeRepository;
        }

        public async Task<Response<int>> Handle(CreatePlaceTypeCommand request, CancellationToken cancellationToken)
        {
            var placeType = new PlaceType
            {
                Name = request.Name,
            };
            await _placetypeRepository.AddAsync(placeType);
            return new Response<int>(placeType.Id);
        }
    }
}
