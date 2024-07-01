using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Features.Districts.Queries.GetDistrictByCityId
{
    public class GetDistrictByCityIdViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CityName { get; set; }
    }
}
