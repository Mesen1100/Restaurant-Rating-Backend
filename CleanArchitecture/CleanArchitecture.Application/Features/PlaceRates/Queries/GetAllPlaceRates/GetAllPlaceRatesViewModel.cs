using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Features.PlaceRates.Queries.GetAllPlaceRates
{
    public class GetAllPlaceRatesViewModel
    {
        public int Id { get; set; }
        public double ServiceRate { get; set; }
        public double HygieneRate { get; set; }
        public string PlaceName { get; set; }
        public string UserName { get; set; }
    }
}
