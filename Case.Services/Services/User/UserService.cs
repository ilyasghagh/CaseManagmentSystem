using Case.Data.Domains;
using Case.Repositry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Case.Services
{
    public class UserService : IUserService
    {
        private IRepository<User> userRepository;
        private readonly IRolesService rolesService;
        private readonly IUserRoleService _userRoleService;
        public UserService(IRepository<User> userRepository,IRolesService rolesService,IUserRoleService userRoleService)
        {
            this.userRepository = userRepository;
            this.rolesService = rolesService;
            _userRoleService = userRoleService;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return userRepository.GetAll();
        }

        public User GetById(int id)
        {
            return userRepository.GetById(id);
        }
        public string GetUserFullNamebyId(int id)
        {
            var user = userRepository.GetById(id);
            return user.FirstName + " " + user.LastName;
        }
        public User GetByEmail(string email)
        {
            return userRepository.GetAll().Where(x => x.Email == email.Trim()).FirstOrDefault();
        }
        public void InsertUser(User user)
        {
            userRepository.Insert(user);
        }
        public void UpdateUser(User user)
        {
            userRepository.Update(user);
        }

        public void DeleteUser(int id)
        {
            User user = userRepository.GetById(id);           
            userRepository.Remove(user);
            userRepository.SaveChanges();
        }
        public List<User> GetAllUsersByrole(string roleName)
        {
            var role = rolesService.GetAllRoles().Where(x => x.RoleName.ToLower().Equals(roleName.ToLower())).FirstOrDefault();
            var userIds = _userRoleService.GetAllRoles().Where(x => x.IsActive && x.RoleId == role.Id).Select(x => x.UserId).ToList();
            return GetAllUsers().Where(x => userIds.Contains(x.Id)).ToList();
        }

    }
}
