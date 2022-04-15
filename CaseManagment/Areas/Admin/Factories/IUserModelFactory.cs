using Case.Data.Domains;
using Case.web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Case.web.Areas.Admin.Factories
{
   public interface IUserModelFactory
    {
        List<UserModel> PrepareUsersList();
        UserModel PrepareModelForUser(User user, UserModel model);
        CityModel PrepareModelForCity(Cities user, CityModel model);
    }
}
