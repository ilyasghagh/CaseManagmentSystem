using Case.Data.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace Case.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User GetById(int id);
        User GetByEmail(string email);
        void InsertUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
        string GetUserFullNamebyId(int id);
        List<User> GetAllUsersByrole(string roleName);
    }
}
