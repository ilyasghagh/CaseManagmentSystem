using Case.Data.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace Case.Services
{
    public interface IRolesService
    {
        IEnumerable<Roles> GetAllRoles();
        Roles GetById(long id);
        void InsertRoles(Roles roles);
        void UpdateRoles(Roles roles);
        void DeleteRoles(int id);
    }
}
