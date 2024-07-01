using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.DTOs.Account
{
    public class ChangePasswordRequest
    {
        public string userId { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}
