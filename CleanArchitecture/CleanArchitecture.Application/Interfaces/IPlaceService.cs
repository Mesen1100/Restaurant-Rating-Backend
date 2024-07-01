using CleanArchitecture.Core.Features.Places.Commands.CreatePlace;
using CleanArchitecture.Core.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IPlaceService
    {
        Task<Response<int>> CreatePlace(CreatePlaceCommand command);
    }
}
