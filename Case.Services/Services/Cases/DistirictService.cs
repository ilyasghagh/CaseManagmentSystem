using Case.Data.Domains;
using Case.Repositry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Case.Services
{
   public class DistirictService:IDistirictService
    {
        private readonly IRepository<Distirict> _repository;
        public DistirictService(IRepository<Distirict> repository)
        {
            _repository = repository;
        }
        public void Delete(Distirict Distirict)
        {
            _repository.Delete(Distirict);
        }

        public IList<Distirict> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public Distirict GetById(int Id)
        {
            return _repository.GetById(Id);
        }

        public void Insert(Distirict Distirict)
        {
            _repository.Insert(Distirict);
        }

        public void Update(Distirict Distirict)
        {
            _repository.Update(Distirict);
            //_repository.SaveChanges();
        }
    }
}
