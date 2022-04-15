using Case.Data.Domains;
using Case.Repositry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Case.Services
{
    public class CaseCategoryService: ICaseCategoryService
    {
        private readonly IRepository<CaseCategory> _repository;
        public CaseCategoryService(IRepository<CaseCategory> repository)
        {
            _repository = repository;
        }
        public void Delete(CaseCategory CaseCategory)
        {
            _repository.Delete(CaseCategory);
        }

        public IList<CaseCategory> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public CaseCategory GetById(int Id)
        {
            return _repository.GetById(Id);
        }

        public void Insert(CaseCategory CaseCategory)
        {
            _repository.Insert(CaseCategory);
        }

        public void Update(CaseCategory CaseCategory)
        {
            _repository.Update(CaseCategory);
            _repository.SaveChanges();
        }
    }
}
