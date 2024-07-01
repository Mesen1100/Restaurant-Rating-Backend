using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Entities
{
    public class Food : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string FoodImage { get; set; }
        public double Price { get; set; }
        public double CommentCount { get; set; }
        public double RatePoint { get; set; }
        public double RateCount { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsShowned { get; set; }
        public virtual FoodType FoodType { get; set; }
        public int FoodTypeId { get; set; }
        public virtual Menu Menu { get; set; }
        public int MenuId { get; set; }
        public virtual List<FoodRate> FoodRates { get; set; }
        public virtual List<FoodComment> FoodComments { get; set; }

    }
}
