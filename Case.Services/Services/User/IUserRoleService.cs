using Case.Data.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace Case.Services
{
    public interface IUserRoleService
    {
        IList<UserRoles> GetAllRolesById(int userId);
        IList<UserRoles> GetAllRoles();
        UserRoles GetById(int Id);
        void Insert(UserRoles userRoles);
        void Delete(UserRoles userRoles);
        void Update(UserRoles userRoles);
        void DeleteAllRolesById(int Id);
    }
}
