using Case.Data.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace Case.Services
{
    public interface ICityService
    {
        IList<Cities> GetAll();
        public IList<Cities> GetAllByProvinceId(int Id);
        Cities GetById(int Id);
        void Insert(Cities Cities);
        void Delete(Cities Cities);
        void Update(Cities Cities);
    }
}
