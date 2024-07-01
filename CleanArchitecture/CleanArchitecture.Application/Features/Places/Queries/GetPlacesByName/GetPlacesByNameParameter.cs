using CleanArchitecture.Core.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Features.Places.Queries.GetPlacesByName
{
    public class GetPlacesByNameParameter:RequestParameter
    {
        public string SearchString { get;set; }
        public int CityId { get; set; }
        public int PlaceTypeId { get; set; }
    }
}
