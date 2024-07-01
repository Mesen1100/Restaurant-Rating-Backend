using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Features.Foods.Commands.UpdateFood
{
    public class UpdateFoodParameter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FoodImage { get; set; }
        public double Price { get; set; }

    }
}
