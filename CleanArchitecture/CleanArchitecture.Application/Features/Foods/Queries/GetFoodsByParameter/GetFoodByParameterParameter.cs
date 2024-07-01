using CleanArchitecture.Core.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Features.Foods.Queries.GetFoodsByMenuId
{
    public class GetFoodByParameterParameter:RequestParameter
    {
        public int FoodTypeId { get; set; }
        public int PlaceId { get; set; }
        public int MenuId { get; set; }
    }
}
