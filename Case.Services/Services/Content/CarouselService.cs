using Case.Data.Domains;
using Case.Repositry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Case.Services
{
    public class CarouselService:ICarouselService
    {
        private readonly IRepository<Carousel> _repository;
        public CarouselService(IRepository<Carousel> repository)
        {
            _repository = repository;
        }
        public void Delete(Carousel Carousel)
        {
            _repository.Delete(Carousel);
        }

        public IList<Carousel> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public Carousel GetById(int Id)
        {
            return _repository.GetById(Id);
        }

        public void Insert(Carousel Carousel)
        {
            _repository.Insert(Carousel);
        }

        public void Update(Carousel Carousel)
        {
            _repository.Update(Carousel);
            //_repository.SaveChanges();
        }
    }
}
