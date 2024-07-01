using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Entities
{
    public class PlaceRate:Rate
    {
        public double ServiceRate { get; set; }
        public double HygieneRate { get; set; }
        public int PlaceId { get; set; }
        public virtual Place Place { get; set; }
    }
}
