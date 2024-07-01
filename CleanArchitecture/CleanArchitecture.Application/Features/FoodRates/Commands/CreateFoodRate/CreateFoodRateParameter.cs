using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Features.FoodRates.Commands.CreateFoodRate
{
    public class CreateFoodRateParameter
    {
        public double PriceRate { get; set; }
        public double TasteRate { get; set; }
        public int FoodId { get; set; }
    }
}
