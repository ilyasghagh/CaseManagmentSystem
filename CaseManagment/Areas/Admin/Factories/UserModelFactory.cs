using Case.Data.Domains;
using Case.Services;
using Case.web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Case.web.Areas.Admin.Factories
{
    public class UserModelFactory:IUserModelFactory
    {
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly IRolesService _rolesService;
        private readonly IProvinceService _provinceService;
        public UserModelFactory(IUserService userService,
            IUserRoleService userRoleService,
            IRolesService rolesService,
            IProvinceService provinceService)
        {
            _userService = userService;
            _userRoleService = userRoleService;
            _rolesService = rolesService;
            _provinceService = provinceService;
        }

        public List<UserModel> PrepareUsersList()
        {
            List<UserModel> userModels = new List<UserModel>();
            var roles = _rolesService.GetAllRoles().ToList();
            var users = _userService.GetAllUsers().ToList();
            if (users.Any())
            {
                foreach (var user in users)
                {
                    var userRoles = _userRoleService.GetAllRolesById(user.Id);
                    foreach (var item in userRoles)
                    {
                        var role = _rolesService.GetById(item.RoleId);
                        var model = new UserModel();
                        model.Email = user.Email;
                        model.FirstName = user.FirstName;
                        model.LastName = user.LastName;
                        model.username = user.username;
                        model.Id = user.Id;
                        model.IsActive = item.IsActive;
                        model.IsDeleted = user.IsDeleted;
                        model.selectedRoles = _userRoleService.GetAllRolesById(user.Id).Select(x => x.RoleId).ToList();
                        model.RoleName = role.RoleName;
                        model.RoleId = role.Id;
                       // model.rolescommaseprated = string.Join(",", roles.Where(x => model.selectedRoles.Contains(x.Id)).Select(x => x.RoleName).ToList());
                        userModels.Add(model);
                    }
                    
                }
            }
            return userModels;
        }
        public UserModel PrepareModelForUser(User user , UserModel model)
        {
            model.roles = _rolesService.GetAllRoles().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.RoleName });
            if (user == null)
                return model;
            else
            {
                model.Email = user.Email;
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.username = user.username;
                model.Phone = user.Phone;              
                model.DateOfBirth = user.DateOfBirth;
                model.IsActive = user.IsActive;
                model.IsDeleted = user.IsDeleted;
                model.Address = user.Address;
                model.Gender = user.Gender;             
                model.CreatedDateUtc = user.CreatedDateUtc;
                model.UpdatedDateUtc = user.UpdatedDateUtc;
                model.FailedLoginAttempts = user.FailedLoginAttempts;
                model.Id = user.Id;
                model.selectedRoles = _userRoleService.GetAllRolesById(user.Id).Select(x => x.RoleId).ToList();
            }
            return model;
        }

        public CityModel PrepareModelForCity(Cities user, CityModel model)
        {
            model.ProvinceList = _provinceService.GetAll().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.ProvinceName }).ToList();
            if (user == null)
                return model;
            else
            {
                model.Id = user.Id;
                model.CityName = user.CityName;
                model.ProvinceId = user.ProvinceId;             
            }
            return model;
        }
    }
}
