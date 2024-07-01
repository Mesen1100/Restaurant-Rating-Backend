using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Entities
{
    public class FoodType:BaseEntity
    {
        public string Name { get; set; }
        public virtual List<Food> Foods { get; set; }
    }
}
