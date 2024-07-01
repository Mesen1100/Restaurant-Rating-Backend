using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Exceptions
{
    public class UserCantCreateException : Exception
    {
        public UserCantCreateException() : base("User Can't Create Try Again")
        {
        }
    }
}
