using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CleanArchitecture.Core.DTOs.Account
{
    public class RegisterPlaceAdminRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string UserName { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string PlaceName { get; set; }
        public string PlaceDescription { get; set; }
        public string PlaceAddress { get; set; }
        public int PlaceCityId { get; set; }
        public int PlaceDistrictId { get; set; }
        public int PlaceTypeId { get; set; }
    }
}
