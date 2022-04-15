using Case.Data.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace Case.Services
{
     public interface ICarouselService
    {
        IList<Carousel> GetAll();
        Carousel GetById(int Id);
        void Insert(Carousel Carousel);
        void Delete(Carousel Carousel);
        void Update(Carousel Carousel);
    }
}
