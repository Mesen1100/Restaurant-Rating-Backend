using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Entities
{
    public class District:BaseEntity
    {
        public string Name { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public virtual List<Place> Places { get; set; }
        
    }
}
