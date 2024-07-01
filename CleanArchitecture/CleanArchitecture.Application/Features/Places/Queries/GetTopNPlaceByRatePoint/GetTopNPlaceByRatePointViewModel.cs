using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Features.Places.Queries.GetTopNPlaceByRatePoint
{
    public class GetTopNPlaceByRatePointViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double RatePoint { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string PlaceTypeName { get; set; }
        
    }
}
