using AutoMapper;
using BookStore.Dtos;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookModels, BookDto>();
            CreateMap<BookDto, BookModels>();
        }
    }
}