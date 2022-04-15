using Case.Data.Domains;
using Case.Services;
using Case.web.Areas.Admin.Factories;
using Case.web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Case.web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly IRolesService _rolesService;
        private readonly IUserModelFactory _userModelFactory;
        public UserController(IUserService userService,
            IUserRoleService userRoleService,
            IRolesService rolesService,
            IUserModelFactory userModelFactory)
        {
            _userService = userService;
            _userRoleService = userRoleService;
            _rolesService = rolesService;
            _userModelFactory = userModelFactory;
        }
        public IActionResult List()
        {
            var model = _userModelFactory.PrepareUsersList();
            return View(model);
        }
        public IActionResult Edit(int Id)
        {
            var user = _userService.GetById(Id);
            var model = _userModelFactory.PrepareModelForUser(user, new Models.UserModel());
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(UserModel model)
        {
            var user = _userService.GetById(model.Id);
            if(user is null)
                return RedirectToAction("List", "User");
            //if (model.selectedRoles.Count == 0)
            //{
            //    ModelState.AddModelError("selectedRoles", "Please select atleast one role");
            //}
            if (ModelState.IsValid)
            {
                user.Email = model.Email;
                user.IsActive = model.IsActive;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Phone = model.Phone;
                user.UpdatedDateUtc = DateTime.UtcNow;
                user.Gender = model.Gender;
                user.FailedLoginAttempts = model.FailedLoginAttempts;
                user.DateOfBirth = model.DateOfBirth;
                user.Address = model.Address;
                //_userRoleService.DeleteAllRolesById(user.Id);
                //foreach (var item in model.selectedRoles)
                //{
                //    var role = new UserRoles();
                //    role.UserId = user.Id;
                //    role.RoleId = item;
                //    role.IsActive = true;
                //    _userRoleService.Insert(role);
                //}
                _userService.UpdateUser(user);
                return RedirectToAction("List", "User");
            }
            model = _userModelFactory.PrepareModelForUser(null, model);
            return View(model);
        }
        public IActionResult ActiveOrDeActive(int id,bool isactive,int roleid)
        {
            //var user = _userService.GetById(id);
            //if(user != null)
            //{
            //    if (isactive)
            //    {
            //        user.IsActive = false;
            //        _userService.UpdateUser(user);
            //    }else
            //    {
            //        user.IsActive = true;
            //        _userService.UpdateUser(user);
            //    }
            //}
            var userRole = _userRoleService.GetAllRolesById(id).FirstOrDefault(x => x.RoleId == roleid);
            if (isactive)
                userRole.IsActive = false;
            else
                userRole.IsActive = true;
            _userRoleService.Update(userRole);
            return RedirectToAction("List", "User");
        }
        public IActionResult DeleteUser(int id,int roleid)
        {
            var user = _userService.GetById(id);
            if (user != null)
            {
                var roles = _userRoleService.GetAllRolesById(id);
                var userRole = _userRoleService.GetAllRolesById(id).FirstOrDefault(x => x.RoleId == roleid);
                if (roles.Count > 1)
                {
                   
                    _userRoleService.Delete(userRole);
                }
                else
                {
                    _userRoleService.Delete(userRole);
                    _userService.DeleteUser(id);
                }               
            }
            return RedirectToAction("List", "User");
        }
    }
}
