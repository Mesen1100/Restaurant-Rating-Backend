﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
