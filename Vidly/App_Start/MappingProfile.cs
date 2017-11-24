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
            Mapper.CreateMap<CustomerDto, Customer>();
        }
    }
}