using AutoMapper;
using GestaoBiblioteca.Core.Entities;
using GestaoBiblioteca.Core.Models.Autor;
using GestaoBiblioteca.Core.Models.Genero;
using GestaoBiblioteca.Core.Models.Livro;
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
            CreateMap<TAutores, AutorDTO>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.COD_TAUTORES))
                 .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.NOME))
                 .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => src.ATIVO))
                 .ReverseMap()
                 .ForMember(dest => dest.COD_TAUTORES, opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.NOME, opt => opt.MapFrom(src => src.Nome))
                 .ForMember(dest => dest.ATIVO, opt => opt.MapFrom(src => src.Ativo));

            CreateMap<TGeneros, GeneroDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.COD_TGENEROS))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.NOME))
                .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => src.ATIVO))
                .ReverseMap()
                .ForMember(dest => dest.COD_TGENEROS, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.NOME, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.ATIVO, opt => opt.MapFrom(src => src.Ativo));

            CreateMap<TLivros, LivroDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.COD_TLIVROS))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.NOME))
                .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => src.ATIVO))
                .ForMember(dest => dest.IdAutor, opt => opt.MapFrom(src => src.COD_TAUTORES))
                .ForMember(dest => dest.IdGenero, opt => opt.MapFrom(src => src.COD_TGENEROS))
                .ReverseMap()
                .ForMember(dest => dest.COD_TLIVROS, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.NOME, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.ATIVO, opt => opt.MapFrom(src => src.Ativo))
                .ForMember(dest => dest.COD_TAUTORES, opt => opt.MapFrom(src => src.IdAutor))
                .ForMember(dest => dest.COD_TGENEROS, opt => opt.MapFrom(src => src.IdGenero));


        }
    }
}
