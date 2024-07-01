using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Exceptions
{
    public class FileNotPictureException:Exception
    {
        public FileNotPictureException(string name)
            : base($"File \"{name}\" is not a image/picture. Please Upload image or picture")
        {
        }
    }
}
