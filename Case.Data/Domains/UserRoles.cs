using System;
using System.Collections.Generic;
using System.Text;

namespace Case.Data.Domains
{
   public class UserRoles:BaseEntity
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
    }
}
