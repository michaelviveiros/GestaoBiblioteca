using AutoMapper;
using GestaoBiblioteca.Core.Entities;
using GestaoBiblioteca.Core.Models.Autor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoBiblioteca.Core.Mappers.ProfilesMappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TAutores, AutorDTO>().ReverseMap();
        }
    }
}
