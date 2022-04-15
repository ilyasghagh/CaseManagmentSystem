using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Case.web.Areas.Admin.Models
{
    public class UserModel
    {
        public UserModel()
        {
            roles = new List<SelectListItem>();
            selectedRoles = new List<int>();
        }
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
       // [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        //[Display(Name = "Confirm Password")]
        //[Compare("Password", ErrorMessage = "Password and confirmation password not match.")]
        public string ConfirmPassword { get; set; }
        public string PasswordSalt { get; set; }
        public string username { get; set; }
        [Required]
        [RegularExpression("^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+.)+[a-z]{2,5}$", ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }
        [Required]
        public string Gender { get; set; }
      
        [Required]
        public string Phone { get; set; }
        [Display(Name = "Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
       // [Required]
       // [Display(Name = "Roles")]
        public List<int> selectedRoles { get; set; }
        public IEnumerable<SelectListItem> roles { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        [Display(Name = "Failed Login Attempts")]
        public int FailedLoginAttempts { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public DateTime UpdatedDateUtc { get; set; }
    }
}
