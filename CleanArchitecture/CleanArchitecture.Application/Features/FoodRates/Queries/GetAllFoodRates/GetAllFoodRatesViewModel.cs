using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Features.FoodRates.Queries.GetAllFoodRates
{
    public class GetAllFoodRatesViewModel
    {
        public int Id { get; set; }
        public double PriceRate { get; set; }
        public double TasteRate { get; set; }
        public string FoodName { get; set; }
        public string UserName { get; set; }
    }
}
