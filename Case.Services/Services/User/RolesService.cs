using Case.Data.Domains;
using Case.Repositry;
using System;
using System.Collections.Generic;
using System.Text;

namespace Case.Services
{ 
    public class RolesService : IRolesService
    {
        private IRepository<Roles> roleRepository;

        public RolesService(IRepository<Roles> roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public IEnumerable<Roles> GetAllRoles()
        {
            return roleRepository.GetAll();
        }

        public Roles GetById(long id)
        {
            return roleRepository.GetById(id);
        }

        public void InsertRoles(Roles Roles)
        {
            roleRepository.Insert(Roles);
        }
        public void UpdateRoles(Roles Roles)
        {
            roleRepository.Update(Roles);
        }

        public void DeleteRoles(int id)
        {
            Roles RolesProfile = roleRepository.GetById(id);         
            roleRepository.Remove(RolesProfile);
            roleRepository.SaveChanges();
        }
    }
}
