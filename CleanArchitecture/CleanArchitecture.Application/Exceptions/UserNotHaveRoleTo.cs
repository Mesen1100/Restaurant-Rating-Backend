using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Exceptions
{
    public class UserNotHaveRoleTo : Exception
    {
        public UserNotHaveRoleTo(string command) : base($"User Not Have Permisson To {command}") { }
    }
}
