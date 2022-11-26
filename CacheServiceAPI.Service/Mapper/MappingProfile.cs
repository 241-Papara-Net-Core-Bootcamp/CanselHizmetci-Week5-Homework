using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CacheServiceAPI.Domain.Entities;
using CacheServiceAPI.Service.DTOs;

namespace CacheServiceAPI.Service.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, PostDTO>().ReverseMap();
        }
    }
}
