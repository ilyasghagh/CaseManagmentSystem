using Case.Data.Domains;
using Case.Repositry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Case.Services
{
    public class CaseService : ICaseService
    {
        private readonly IRepository<Cases> _repositry;
        public CaseService(IRepository<Cases> repository)
        {
            _repositry = repository;
        }
        public IList<Cases> GetAllbyFilters(int LawerId=0, int JudgeId=0,
            int CaseCategoryId=0, int PoliceStationId = 0, int CaseNumber=0,string Name=null,string FirNo=null,int courtId= 0,int districtId=0)
        {
            var data = _repositry.GetAll();
            if (LawerId > 0)
                data = data.Where(x => x.LawyerId.Equals(LawerId));
            if (JudgeId > 0)
                data = data.Where(x => x.JudgeId.Equals(JudgeId));
            if (CaseCategoryId > 0)
                data = data.Where(x => x.CategoryId.Equals(CaseCategoryId));
            if (PoliceStationId > 0)
                data = data.Where(x => x.PoliceStationId.Equals(PoliceStationId));
            if (CaseNumber > 0)
                data = data.Where(x => x.Id.Equals(CaseNumber));
            if (!string.IsNullOrEmpty(FirNo))
                data = data.Where(x => x.FirNo.Contains(FirNo));
            if (courtId > 0)
                data = data.Where(x => x.CourtId.Equals(courtId));
            if (districtId > 0)
                data = data.Where(x => x.DistrictId.Equals(districtId));
            if (!string.IsNullOrEmpty(Name))
                data = data.Where(x => x.Name.Contains(Name));
            return data.ToList();
        }
        public void Delete(Cases Cases)
        {
            _repositry.Delete(Cases);
        }

        public IList<Cases> GetAll()
        {
            return _repositry.GetAll().ToList();
        }

        public Cases GetById(int Id)
        {
            return _repositry.GetById(Id);
        }

        public void Insert(Cases Cases)
        {
            _repositry.Insert(Cases);
        }

        public void Update(Cases Cases)
        {
            _repositry.Update(Cases);
            _repositry.SaveChanges();
        }
    }
}
