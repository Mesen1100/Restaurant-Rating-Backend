using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Entities
{
    public class FoodComment:Comment
    {
        public int FoodId { get; set; }
        public virtual Food Food { get; set; }
    }
}
