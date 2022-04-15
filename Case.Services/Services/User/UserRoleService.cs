using Case.Data.Domains;
using Case.Repositry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Case.Services
{
    public class UserRoleService : IUserRoleService
    {
        private IRepository<UserRoles> roleRepository;

        public UserRoleService(IRepository<UserRoles> roleRepository)
        {
            this.roleRepository = roleRepository;
        }
        public void Delete(UserRoles userRoles)
        {
            roleRepository.Remove(userRoles);
            roleRepository.SaveChanges();
        }

        public void DeleteAllRolesById(int Id)
        {
            var roles = GetAllRolesById(Id);
            if (roles.Count > 0)
                roles.ToList().ForEach(x => Delete(x));
        }

        public IList<UserRoles> GetAllRoles()
        {
          return  roleRepository.GetAll().ToList();
        }

        public IList<UserRoles> GetAllRolesById(int userId)
        {
            return roleRepository.GetAll().Where(x => x.UserId == userId).ToList();
        }

        public UserRoles GetById(int Id)
        {
            return roleRepository.GetById(Id);
        }

        public void Insert(UserRoles userRoles)
        {
            roleRepository.Insert(userRoles);
        }

        public void Update(UserRoles userRoles)
        {
            roleRepository.Update(userRoles);
        }
    }
}
