using Case.Data.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace Case.Services
{
    public interface IPoliceStationService
    {
        IList<PoliceStation> GetAll();
        PoliceStation GetById(int Id);
        void Insert(PoliceStation policeStation);
        void Delete(PoliceStation policeStation);
        void Update(PoliceStation policeStation);
    }
}
