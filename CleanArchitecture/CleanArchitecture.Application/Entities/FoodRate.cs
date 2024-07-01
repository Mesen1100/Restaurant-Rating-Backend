using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Entities
{
    public class FoodRate:Rate
    {
        public double PriceRate { get; set; }
        public double TasteRate { get; set; }
        public int FoodId { get; set; }
        public virtual Food Food { get; set; }
    }
}
