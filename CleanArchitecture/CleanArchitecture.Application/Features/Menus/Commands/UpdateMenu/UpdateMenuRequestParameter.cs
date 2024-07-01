using CleanArchitecture.Core.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Features.Menus.Commands.UpdateMenu
{
    public class UpdateMenuRequestParameter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MenuTypeId { get; set; }
    }
}
