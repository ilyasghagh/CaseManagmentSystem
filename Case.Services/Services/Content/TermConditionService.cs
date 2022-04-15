using Case.Data.Domains;
using Case.Repositry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Case.Services
{
    public class TermConditionService:ITermConditionService
    {
        private readonly IRepository<TermCondition> _repository;
        public TermConditionService(IRepository<TermCondition> repository)
        {
            _repository = repository;
        }
        public void Delete(TermCondition TermCondition)
        {
            _repository.Delete(TermCondition);
        }

        public IList<TermCondition> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public TermCondition GetById(int Id)
        {
            return _repository.GetById(Id);
        }

        public void Insert(TermCondition TermCondition)
        {
            _repository.Insert(TermCondition);
        }

        public void Update(TermCondition TermCondition)
        {
            _repository.Update(TermCondition);
        }
    }
}
