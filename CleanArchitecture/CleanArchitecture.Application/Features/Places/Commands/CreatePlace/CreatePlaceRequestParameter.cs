using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Features.Places.Commands.CreatePlace
{
    public class CreatePlaceRequestParameter
    {     
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public int PlaceTypeId { get; set; }
    }
}
