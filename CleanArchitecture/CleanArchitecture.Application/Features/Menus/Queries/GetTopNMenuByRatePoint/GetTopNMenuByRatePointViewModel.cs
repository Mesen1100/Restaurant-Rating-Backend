using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Features.Menus.Queries.GetTopNMenuByRatePoint
{
    public class GetTopNMenuByRatePointViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double MenuRate { get; set; }
        public string PlaceName { get; set; }
        public string MenuTypeName { get; set; }
    }
}
