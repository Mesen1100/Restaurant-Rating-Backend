using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Features.Menus.Commands.CreateMenu
{
    public class CreateMenuRequestParameter
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int PlaceId { get; set; }
        public int MenuTypeId { get; set; }
    }
}
