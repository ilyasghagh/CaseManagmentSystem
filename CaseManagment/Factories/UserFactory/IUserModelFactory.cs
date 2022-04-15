using Case.Data.Domains;
using Case.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Case.web.Factories.UserFactory
{
   public interface IUserModelFactory
    {
        UserViewModel PrepareUserModel(User user, UserViewModel model);
        LawyerSearchModel PrepareLawyerList(LawyerSearchModel model);
        CaseViewModel PrepareCaseModel(Cases cas, CaseViewModel model);
        void InsertNewCase(CaseViewModel cas);
        void UpdateCase(CaseViewModel cas);
    }
}
