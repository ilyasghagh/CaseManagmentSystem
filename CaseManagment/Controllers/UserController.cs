using Case.Services;
using Case.web.Factories.UserFactory;
using Case.web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using Case.Data.Domains;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Case.web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;       
        private readonly IUserModelFactory _userModelFactory;
        private readonly IUserRoleService _userRoleService;
        private readonly IRolesService  _rolesService;
        public UserController(IUserService userService,IUserModelFactory userModelFactory,
            IUserRoleService userRoleService,
            IRolesService rolesService)
        {
            _userService = userService;
            _userModelFactory = userModelFactory;
            _userRoleService = userRoleService;
            _rolesService = rolesService;
        }
       
        public IActionResult Register(int? id)
        {
            var user = _userService.GetById(id ?? 0);

            var model = _userModelFactory.PrepareUserModel(user,new UserViewModel());
            return View(model);
        }
        [HttpPost]
        public IActionResult Register(UserViewModel userViewModel)
        {
            bool ok = false;
            string message = string.Empty;
            var olduser = _userService.GetByEmail(userViewModel.Email);
            if(olduser != null)
            {
              message = "Email is already in use please try a different";
              ModelState.AddModelError("Email", "Email is already in use please try a different");
            }
            if(!string.IsNullOrEmpty(userViewModel.Phone))
            {
                olduser = _userService.GetAllUsers().Where(x => x.Phone.Equals(userViewModel.Phone.Trim())).FirstOrDefault();
                if (olduser != null)
                {
                    message = "Mobile is already in use please try a different";
                    ModelState.AddModelError("Phone", "Mobile is already in use please try a different");
                }
            }
           
            if (userViewModel.selectedRoles.Count == 0)
            {
                ModelState.AddModelError("selectedRoles", "Please select atleast one role");
            }
           
            if (ModelState.IsValid)
            {
                string salt = Crypto.GenerateSalt();
                string password = userViewModel.Password + salt;
                string hashedPassword = Crypto.HashPassword(password);
                var User = new User();
                User.Password = hashedPassword;
                User.PasswordSalt = salt;
                User.username = userViewModel.Email;
                User.Email = userViewModel.Email;
                User.FailedLoginAttempts = 0;
                User.FirstName = userViewModel.FirstName;
                User.LastName = userViewModel.LastName;
                User.CreatedDateUtc = DateTime.UtcNow;
                User.UpdatedDateUtc = DateTime.UtcNow;
                User.Gender = userViewModel.Gender;
                User.DateOfBirth = userViewModel.DateOfBirth;
                User.Phone = userViewModel.Phone;
                User.IsActive = true;
                User.IsDeleted = false;
                User.Address = userViewModel.Address;           
                _userService.InsertUser(User);
                foreach (var item in userViewModel.selectedRoles)
                {
                    var role = new UserRoles();
                    role.UserId = User.Id;
                    role.RoleId = item;
                    role.IsActive = false;
                    _userRoleService.Insert(role);
                }
                ok = true;
                ViewBag.msg = "Account created succefully but not active contact to admin for account activation";
            }
            var model = _userModelFactory.PrepareUserModel(null , userViewModel);
            return View(model);
           // return Json(new { ok = ok, message=message});
        }
        public IActionResult UserProfile(int id)
        {
            var user = _userService.GetById(id);

            var model = _userModelFactory.PrepareUserModel(user, new UserViewModel());
            return View(model);
        }
        [HttpPost]
        public IActionResult UserProfile(UserViewModel userViewModel)
        {
            var user = _userService.GetById(userViewModel.Id);
            if(ModelState.IsValid)
            {
                user.Email = userViewModel.Email;
                user.FirstName = userViewModel.FirstName;
                user.LastName = userViewModel.LastName;
                user.Phone = userViewModel.Phone;
                user.UpdatedDateUtc = DateTime.UtcNow;
                user.Gender = userViewModel.Gender;
                user.DateOfBirth = userViewModel.DateOfBirth;
                user.Address = userViewModel.Address;
                _userService.UpdateUser(user);
                ViewBag.msg = "Updated";
            }
            
            var model = _userModelFactory.PrepareUserModel(null, userViewModel);
            return View(model);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {           
            if (ModelState.IsValid)
            {
                var user = _userService.GetByEmail(loginViewModel.Email);
                
                if (user != null)
                {
                    if (VerifyPassword(loginViewModel.Password, user.Password, user.PasswordSalt))
                    {
                        if (!user.IsActive)
                        {
                            ModelState.AddModelError("", "Account is not active");
                            return View();
                        }
                          
                        var roles = _userRoleService.GetAllRolesById(user.Id);
                        if(roles.Count > 0)
                        {
                            if(roles.Count == 1)
                            {
                                var role = _rolesService.GetById(roles.FirstOrDefault().RoleId);
                                LoginCustomer(user, role.Id);
                                if (role.RoleName.ToLower().Equals("admin"))
                                    return RedirectToAction("Index", "Home", new { Area = "Admin" });
                                else
                                    return RedirectToAction("ConfirmRole", "User", new { userId = user.Id });
                            }
                            else
                            {
                                return RedirectToAction("ConfirmRole", "User",new { userId = user.Id});
                            }
                        }             
                       
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Password is incorrect");
                    }
                }
                else
                {
                    ModelState.AddModelError("Password", "Account not found");
                }
            }
           
            return View();
        }
        public IActionResult ConfirmRole(int userId,bool isroleActive = true)
        {
            var userrole = _userRoleService.GetAllRolesById(userId);           
            var role = _rolesService.GetAllRoles();
            role = role.Where(x => userrole.Select(d => d.RoleId).Contains(x.Id));
            var  newrole = role.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.RoleName });
            var model = new CustomerRoleSelectorModel()
            {
                userId = userId,
                roles = newrole,
                roleId = 0
            };
            if (!isroleActive)
                ViewBag.roleActive = "You cannot login because role is not active. Please contact admin for activation";
            return View(model);
        }
        [HttpPost]
        public IActionResult ConfirmRole(CustomerRoleSelectorModel model)
        {
            if (ModelState.IsValid)
            {
                var userRole = _userRoleService.GetAllRolesById(model.userId).FirstOrDefault(x => x.RoleId == model.roleId);
                if (!userRole.IsActive)
                    return RedirectToAction("ConfirmRole", "User", new { userId = model.userId, isroleActive= false });
                var user = _userService.GetById(model.userId);

                LoginCustomer(user, model.roleId);
                var role = _rolesService.GetById(model.roleId);
                if (role.RoleName.ToLower().Equals("admin"))
                    return RedirectToAction("Index", "Home", new { Area = "Admin" });
                else if(role.RoleName.ToLower().Equals("lawyer"))
                    return RedirectToAction("Index", "Lawyer");
                else
                    return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
        public bool VerifyPassword(string password,string hash , string salt)
        {
            var pass = password + salt;
            var verify = Crypto.VerifyHashedPassword(hash, pass);
            return verify;
        }

        public void LoginCustomer(User user , int roleId)
        {
            var role = _rolesService.GetById(roleId);
            var claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.Name,user.Email),
                            new Claim("FullName",user.FirstName + " " + user.LastName),
                            new Claim("Id",user.Id.ToString()),
                            new Claim("RoleId",roleId.ToString()),
                            new Claim(ClaimTypes.Role,role.RoleName),
                        };
            var identit = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principle = new ClaimsPrincipal(identit);
            var props = new AuthenticationProperties();
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle, props).Wait();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgotPassword(string email)
        {
            if(!string.IsNullOrEmpty(email))
            {
                var user = _userService.GetByEmail(email.Trim());
                if (user is null)
                {
                    ModelState.AddModelError("email", "Account not found on provided email please enter a valid email");
                    return View();
                }   
                else
                {
                    return RedirectToAction("PasswordSuccesfullySent", "User");
                }
            }
            else
            {
                ModelState.AddModelError("email", "Please Enter a valid email");
            }
            return View();
        }   
        public IActionResult PasswordSuccesfullySent()
        {
            return View();
        }
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult ChangePassword(string oldpassword , string password,string confirmpassword)
        {
            if(string.IsNullOrEmpty(oldpassword)|| string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmpassword))
            {
                ModelState.AddModelError("password", "Please fill out all fields");
                return View();
            }
            if(password != confirmpassword)
            {
                ModelState.AddModelError("password", "password and confirm password fields not matched");
                return View();
            }
             var userId = Convert.ToInt32(HttpContext.User.Claims.Where(c => c.Type == "Id")
                   .Select(c => c.Value).SingleOrDefault());
            var user = _userService.GetById(userId);
            if(VerifyPassword(oldpassword,user.Password,user.PasswordSalt))
            {
                var salt = Crypto.GenerateSalt();
                var pass = password + salt;
                var newPassword = Crypto.HashPassword(pass);
                user.Password = newPassword;
                user.PasswordSalt = salt;
                _userService.UpdateUser(user);
                ViewBag.message = "Password changed successfully";
            }
            else
            {
                ModelState.AddModelError("password", "Old password is not correct");
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await  HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
