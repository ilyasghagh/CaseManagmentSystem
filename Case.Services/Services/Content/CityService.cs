using Case.Data.Domains;
using Case.Repositry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Case.Services
{
    public class CityService : ICityService
    {
        private readonly IRepository<Cities> _repository;
        public CityService(IRepository<Cities> repository)
        {
            _repository = repository;
        }
        public void Delete(Cities Cities)
        {
            _repository.Delete(Cities);
        }

        public IList<Cities> GetAll()
        {
            return _repository.GetAll().ToList();
        }
        public IList<Cities> GetAllByProvinceId(int Id)
        {
            return _repository.GetAll().Where(x => x.ProvinceId.Equals(Id)).ToList();
        }
        public Cities GetById(int Id)
        {
            return _repository.GetById(Id);
        }

        public void Insert(Cities Cities)
        {
            _repository.Insert(Cities);
        }

        public void Update(Cities Cities)
        {
            _repository.Update(Cities);
        }
    }
}
