using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Exceptions
{
    public class UserNotPlaceAdminException:Exception
    {
        public UserNotPlaceAdminException(string name)
            : base($"{name} doesn't have a place")
        {
        }
    }
}
