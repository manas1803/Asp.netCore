using ParkyApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyApi.Repository.IRepository
{
    public interface INationalPark
    {
        ICollection<NationalPark> GetNationalParks();
        NationalPark GetNationalPark(int nationalParkId);
        bool NationalParkExists(string name);
        bool NationalParkExists(int id);
        bool CreateNationalPark(NationalPark national);
        bool UpdateNationalPark(NationalPark national);
        bool DeleteNationalPark(NationalPark national);
        bool Save();
    }
}
