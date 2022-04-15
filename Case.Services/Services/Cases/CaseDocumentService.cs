using Case.Data.Domains;
using Case.Repositry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Case.Services
{
   public class CaseDocumentService:ICaseDocumentService
    {
        private readonly IRepository<CaseDocuments> _repository;
        public CaseDocumentService(IRepository<CaseDocuments> repository)
        {
            _repository = repository;
        }
        public void Delete(CaseDocuments CaseDocuments)
        {
            _repository.Delete(CaseDocuments);
        }

        public IList<CaseDocuments> GetAll()
        {
            return _repository.GetAll().ToList();
        }
       public IList<CaseDocuments> GetAllByCaseId(int caseId)
        {
            return GetAll().Where(x => x.CaseId == caseId).ToList();
        }
        public CaseDocuments GetById(int Id)
        {
            return _repository.GetById(Id);
        }

        public void Insert(CaseDocuments CaseDocuments)
        {
            _repository.Insert(CaseDocuments);
        }

        public void Update(CaseDocuments CaseDocuments)
        {
            _repository.Update(CaseDocuments);
            _repository.SaveChanges();
        }
    }
}
