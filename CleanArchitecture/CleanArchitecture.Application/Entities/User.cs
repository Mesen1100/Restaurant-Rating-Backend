using CleanArchitecture.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public virtual List<FoodRate> FoodRates { get; set; }
        public virtual List<PlaceRate> PlaceRates { get; set; }
        public virtual List<FoodComment> FoodComments { get; set; }
        public virtual List<PlaceComment> PlaceComments { get; set; }
        public virtual List<Place> Places { get; set; }
    }
}
