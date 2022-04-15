using Case.Data.Domains;
using Case.Repositry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Case.Services
{
    public class PoliceStationService : IPoliceStationService
    {
        private readonly IRepository<PoliceStation> _repository;
        public PoliceStationService(IRepository<PoliceStation> repository)
        {
            _repository = repository;
        }
        public void Delete(PoliceStation policeStation)
        {
            _repository.Delete(policeStation);
        }

        public IList<PoliceStation> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public PoliceStation GetById(int Id)
        {
            return _repository.GetById(Id);
        }

        public void Insert(PoliceStation policeStation)
        {
            _repository.Insert(policeStation);
        }

        public void Update(PoliceStation policeStation)
        {
            _repository.Update(policeStation);
            //_repository.SaveChanges();
        }
    }
}
