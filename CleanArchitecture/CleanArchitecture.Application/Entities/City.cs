using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Entities
{
    public class City:BaseEntity
    {
        public string Name { get; set; }
        public virtual List<District> Districts { get; set; }
        public virtual List<Place> Places { get; set; } 

    }
}
