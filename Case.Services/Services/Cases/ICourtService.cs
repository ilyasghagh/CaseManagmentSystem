using Case.Data.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace Case.Services
{
    public interface ICourtService
    {
        IList<Court>GetAll();
        Court GetById(int Id);
        void Insert(Court Court);
        void Delete(Court Court);
        void Update(Court Court);
    }
}
