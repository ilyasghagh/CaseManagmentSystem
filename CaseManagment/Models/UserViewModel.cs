using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.WebPages.Html;

namespace Case.web.Models
{
    public class UserViewModel
    {
        public UserViewModel()
        {         
            roles = new List<SelectListItem>();
            selectedRoles = new List<int>();
        }
        public int Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{6,}$",ErrorMessage = "Minimum six characters, at least one letter, one number and one special character")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password not match.")]
        public string ConfirmPassword { get; set; }
        public string PasswordSalt { get; set; }     
        public string username { get; set; }
        [Required]
        [RegularExpression("^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+.)+[a-z]{2,5}$",ErrorMessage ="Please enter a valid email")]
        public string Email { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }      
        [Required]
        [Display(Name = "Roles")]
        public List<int> selectedRoles { get; set; }
        public IEnumerable<SelectListItem> roles { get; set; } 
    }
    public class CustomerRoleSelectorModel
    {
        public CustomerRoleSelectorModel()
        {
            roles = new List<SelectListItem>();
        }
        [Required]
        public int userId { get; set; }
        [Required]
        public int roleId { get; set; }
        public IEnumerable<SelectListItem> roles { get; set; }
    }
}
