using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Features.Foods.Queries.GetTopNFoodByRatePoint
{
    public class GetTopNFoodByRatePointViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FoodImage { get; set; }
        public double Price { get; set; }
        public double RatePoint { get; set; }
        public string FoodTypeName { get; set; }
    }
}
