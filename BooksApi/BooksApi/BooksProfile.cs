using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksApi
{
    public class BooksProfile : Profile
    {
        public BooksProfile()
        {
            CreateMap<Entities.Book, Model.Book>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src =>
                       $"{src.Author.FirstName} {src.Author.LastName}"));

            CreateMap<Entities.Book, Model.BookForCreation>();
        }
    }
}
