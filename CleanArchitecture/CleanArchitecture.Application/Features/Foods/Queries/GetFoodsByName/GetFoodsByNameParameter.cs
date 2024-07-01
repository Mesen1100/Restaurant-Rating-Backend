using CleanArchitecture.Core.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Features.Foods.Queries.GetFoodsByName
{
    public class GetFoodsByNameParameter:RequestParameter
    {
        public string SearchString { get; set; }
    }
}
