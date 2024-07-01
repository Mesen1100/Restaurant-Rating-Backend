using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Entities
{
    public class Category:AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
