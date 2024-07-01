using CleanArchitecture.Core.Features.Places.Commands.CreatePlace;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Services
{
    public class PlaceService:IPlaceService
    {
        private readonly IMediator _mediator;

        public PlaceService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Response<int>> CreatePlace(CreatePlaceCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
