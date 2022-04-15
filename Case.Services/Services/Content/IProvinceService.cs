using Case.Data.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace Case.Services
{
    public interface IProvinceService
    {
        IList<Provinces> GetAll();
        Provinces GetById(int Id);
        void Insert(Provinces Provinces);
        void Delete(Provinces Provinces);
        void Update(Provinces Provinces);
    }
}
