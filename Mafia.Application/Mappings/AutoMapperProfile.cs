using AutoMapper;
using Mafia.Domain.Entities;
using System;

namespace Mafia.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Country, Country>();
            CreateMap<Country, Country>();

        }
    }
}