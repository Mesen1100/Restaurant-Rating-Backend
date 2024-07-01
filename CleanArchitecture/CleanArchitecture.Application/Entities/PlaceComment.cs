using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Entities
{
    public class PlaceComment:Comment
    {
        public int PlaceId { get; set; }
        public virtual Place Place { get; set; }
    }
}
