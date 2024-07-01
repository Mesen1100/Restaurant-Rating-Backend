using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Features.Foods.Queries.GetAllFoods
{
    public class GetAllFoodsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FoodImage { get; set; }
        public double Price { get; set; }
        public double CommentCount { get; set; }
        public double RatePoint { get; set; }
        public double RateCount { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsShowned { get; set; }
        public string FoodTypeName { get; set; }
        public string MenuName { get; set; }
        public string PlaceName { get; set; }
    }
}
