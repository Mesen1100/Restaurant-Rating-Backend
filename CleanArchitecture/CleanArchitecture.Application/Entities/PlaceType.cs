using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Entities
{
    public class PlaceType:BaseEntity
    {
        public string Name { get; set; }
        // :TODO Add a normalized name for unique name
        public virtual List<Place> Places { get; set; }
    }
}
