using CleanArchitecture.Core.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Features.Menus.Queries.GetMenusByName
{
    public class GetMenusByNameParameter:RequestParameter
    {
        public string SearchString { get; set; }
    }
}
