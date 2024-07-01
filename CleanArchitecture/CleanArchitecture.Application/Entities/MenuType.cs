using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Entities
{
    public class MenuType:BaseEntity
    {
        public string Name { get; set; }
        public virtual List<Menu> Menus { get; set; }
    }
}
