using CleanArchitecture.Core.Filters;
using Google.Apis.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Features.Menus.Queries.GetMenuByPlaceId
{
    public class GetMenuByPlaceIdParameter:RequestParameter
    {
        public int PlaceId { get; set; }
        public int MenuTypeId { get; set; }
    }
}
