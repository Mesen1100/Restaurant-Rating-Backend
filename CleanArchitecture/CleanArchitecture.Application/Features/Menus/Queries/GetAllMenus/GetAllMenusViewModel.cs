using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Features.Menus.Queries.GetAllMenus
{
    public class GetAllMenusViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double MenuRate { get; set; }
        public double MenuRateCount { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsShowned { get; set; }
        public string PlaceName { get; set; }
        public string MenuTypeName { get; set; }
    }
}
