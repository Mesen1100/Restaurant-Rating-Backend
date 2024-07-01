using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Features.Foods.Commands.CreateFood
{
    public class CreateFoodParameter
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string FoodImage { get; set; }
        public double Price { get; set; }
        public int FoodTypeId { get; set; }
        public int MenuId { get; set; }
    }
}
