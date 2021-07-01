using AutoMapper;
using ParkyApi.Model;
using ParkyApi.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyApi.Mapper
{
    public class ParkyMapper:Profile
    {
        public ParkyMapper()
        {
            CreateMap<NationalPark, NationalParkDto>().ReverseMap();
        }
    }
}
