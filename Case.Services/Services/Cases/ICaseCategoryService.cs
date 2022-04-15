using Case.Data.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace Case.Services
{
   public interface ICaseCategoryService
    {
        IList<CaseCategory> GetAll();
        CaseCategory GetById(int Id);
        void Insert(CaseCategory CaseCategory);
        void Delete(CaseCategory CaseCategory);
        void Update(CaseCategory CaseCategory);
    }
}
