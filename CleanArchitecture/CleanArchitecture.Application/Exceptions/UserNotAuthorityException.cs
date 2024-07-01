using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Exceptions
{
    public class UserNotAuthorityException:Exception
    {
        public UserNotAuthorityException(string name)
            : base($"{name} doesn't have enough Role to do that operation")
        {
        }
    }
}
