using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.App_Start
{
    public class MappingProfile : Profile
    {
        //automapper jest to rozszerzenie do mapowania podobnych klas np. viewmodel z modelem
        // wykorzystuje sie do tego aby nie pisac duzej ilosci kodu, np. inicjalizowanie viewModel lub Dtos
        // np. CustomerDto.Name = customer.Name itd. az do zainicjalizowania wszystkich pol
        public MappingProfile()
        {
            Mapper.CreateMap<Customer, CustomerDto>();
            //podczas mapowanaia automapper przy edycji danej tabeli np. customers, movies nie moze zmieniac Id
            //dlatego informuje sie go aby pominal klucz id
            Mapper.CreateMap<CustomerDto, Customer>().ForMember(c => c.Id, opt => opt.Ignore());

            Mapper.CreateMap<Movies, MovieDto>();
            Mapper.CreateMap<MovieDto, Movies>().ForMember(c => c.Id, opt => opt.Ignore());

            Mapper.CreateMap<MembershipTypes, MembershipTypeDto>();
        }
    }
}