using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Entities
{
    public class Comment :AuditableBaseEntity
    {
        public string Message { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsShowned { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
