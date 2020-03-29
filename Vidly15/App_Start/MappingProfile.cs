using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly15.Dto;
using Vidly15.Models;

namespace Vidly15.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Customer maping
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<CustomerDto, Customer>().ForMember(c => c.Id, opt => opt.Ignore());

            //Movie mapping
            Mapper.CreateMap<Movie, MovieDto>();
            Mapper.CreateMap<MovieDto, Movie>().ForMember(m => m.Id, opt => opt.Ignore());

            //MembershipType mapping
            Mapper.CreateMap<MembershipType, MembershipTypeDto>();

            //Genre mapping
            Mapper.CreateMap<Genre, GenreDto>();
        }
    }
}