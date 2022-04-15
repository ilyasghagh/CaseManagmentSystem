using Case.Data.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Case.Repositry
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T GetById(long id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Remove(T entity);
        void SaveChanges();       
    }
}
