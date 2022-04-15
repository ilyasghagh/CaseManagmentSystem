using System;
using System.Collections.Generic;
using System.Text;

namespace Case.Data.Domains
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string username { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int FailedLoginAttempts { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public DateTime UpdatedDateUtc { get; set; }
        public string Gender { get; set; }       
        public string Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
