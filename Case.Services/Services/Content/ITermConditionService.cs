using Case.Data.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace Case.Services
{
    public interface ITermConditionService
    {
        IList<TermCondition> GetAll();
        TermCondition GetById(int Id);
        void Insert(TermCondition TermCondition);
        void Delete(TermCondition TermCondition);
        void Update(TermCondition TermCondition);
    }
}
