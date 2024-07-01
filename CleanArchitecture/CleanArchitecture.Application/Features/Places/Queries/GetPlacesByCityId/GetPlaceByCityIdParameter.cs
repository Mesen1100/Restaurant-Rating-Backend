using CleanArchitecture.Core.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Features.Places.Queries.GetPlacesByCityId
{
    public class GetPlaceByCityIdParameter:RequestParameter
    { 
        public int CityId { get; set; }
        public int PlaceTypeId { get; set; }
    }
}
