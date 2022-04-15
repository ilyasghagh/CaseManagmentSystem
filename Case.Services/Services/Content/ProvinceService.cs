using Case.Data.Domains;
using Case.Repositry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Case.Services
{
    public class ProvinceService : IProvinceService
    {
        private readonly IRepository<Provinces> _repository;
        public ProvinceService(IRepository<Provinces> repository)
        {
            _repository = repository;
        }
        public void Delete(Provinces Provinces)
        {
            _repository.Delete(Provinces);
        }

        public IList<Provinces> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public Provinces GetById(int Id)
        {
            return _repository.GetById(Id);
        }

        public void Insert(Provinces Provinces)
        {
            _repository.Insert(Provinces);
        }

        public void Update(Provinces Provinces)
        {
            _repository.Update(Provinces);
        }
    }
}
