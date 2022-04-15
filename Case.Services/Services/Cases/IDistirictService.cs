using Case.Data.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace Case.Services
{
   public interface IDistirictService
    {
        IList<Distirict> GetAll();
        Distirict GetById(int Id);
        void Insert(Distirict Distirict);
        void Delete(Distirict Distirict);
        void Update(Distirict Distirict);
    }
}
