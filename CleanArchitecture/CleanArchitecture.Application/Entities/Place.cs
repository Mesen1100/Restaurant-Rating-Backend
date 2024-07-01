using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Entities
{
    public class Place: AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; } 
        public virtual List<Menu> Menus { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public virtual City City { get; set; }
        public virtual District District { get; set; }
        public double RatePoint { get; set; }   
        public double RateCount { get; set; }
        public double CommentCount { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsShowned { get; set; }
        public int PlaceTypeId { get; set; }
        public virtual PlaceType PlaceType { get; set; }
        public virtual List<PlaceRate> PlaceRates { get; set; }
        public virtual List<PlaceComment> PlaceComments { get; set; }
        public string ManagerUserId { get; set; }
        public virtual User User { get; set; }

    }
}
