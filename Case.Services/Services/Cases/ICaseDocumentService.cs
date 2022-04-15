using Case.Data.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace Case.Services
{ 
    public interface ICaseDocumentService
    {
        IList<CaseDocuments> GetAll();
        IList<CaseDocuments> GetAllByCaseId(int caseId);
        CaseDocuments GetById(int Id);
        void Insert(CaseDocuments CaseDocuments);
        void Delete(CaseDocuments CaseDocuments);
        void Update(CaseDocuments CaseDocuments);
    }
}
