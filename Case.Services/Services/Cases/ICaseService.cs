using System;
using System.Collections.Generic;
using System.Text;
using Case.Data.Domains;

namespace Case.Services
{
   public interface ICaseService
    {
        IList<Cases> GetAllbyFilters(int LawerId = 0, int JudgeId = 0,
            int CaseCategoryId = 0, int PoliceStationId = 0, int CaseNumber = 0, string Name = null, string FirNo = null, int courtId = 0, int districtId = 0);
        IList<Cases> GetAll();
        Cases GetById(int Id);
        void Insert(Cases Cases);
        void Delete(Cases Cases);
        void Update(Cases Cases);
    }
}
