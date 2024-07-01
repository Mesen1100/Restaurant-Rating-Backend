using CleanArchitecture.Core.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Features.Places.Queries.GetPlaceByDistrictId
{
    public class GetPlaceByDistrictIdParameter:RequestParameter
    {
        public int DistrictId { get; set; }
    }
}
