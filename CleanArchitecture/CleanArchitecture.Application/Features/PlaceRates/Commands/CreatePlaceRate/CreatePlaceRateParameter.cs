using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Features.PlaceRates.Commands.CreatePlaceRate
{
    public class CreatePlaceRateParameter
    {
        public double ServiceRate { get; set; }
        public double HygieneRate { get; set; }
        public int PlaceId { get; set; }
    }
}
