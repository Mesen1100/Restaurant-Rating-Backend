using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Features.Places.Queries.GetAllPlaces
{
    public class GetAllPlacesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double RatePoint { get; set; }
        public double RateCount { get; set; }
        public string Address { get; set; }
        public string CityName { get; set; }
        public string DistrictName { get; set; }
        public string PlaceTypeName { get; set; }
        public string ManagerName { get; set; }
    }
}
