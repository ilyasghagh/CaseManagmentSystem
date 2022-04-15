using Case.Data.Domains;
using Case.Repositry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Case.Services
{
    public class CourtService : ICourtService
    {
        private readonly IRepository<Court> _repository;
        public CourtService(IRepository<Court> repository)
        {
            _repository = repository;
        }
        public void Delete(Court Court)
        {
            _repository.Delete(Court);
        }

        public IList<Court> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public Court GetById(int Id)
        {
            return _repository.GetById(Id);
        }

        public void Insert(Court Court)
        {
            _repository.Insert(Court);
        }

        public void Update(Court Court)
        {
            _repository.Update(Court);
            //_repository.SaveChanges();
        }
    }
}
