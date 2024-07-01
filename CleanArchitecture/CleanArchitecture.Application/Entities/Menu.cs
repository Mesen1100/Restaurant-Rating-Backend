using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Entities
{
    public class Menu :AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<Food> Foods { get; set; }
        public double MenuRate { get; set; }
        public double MenuRateCount { get;set; }
        public bool IsEnabled { get; set; }
        public bool IsShowned { get; set; }
        public virtual Place Place { get; set; }
        public int PlaceId { get; set; }
        public virtual MenuType MenuType { get; set; }
        public int MenuTypeId { get; set; }
    }
}
