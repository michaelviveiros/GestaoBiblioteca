using AutoMapper;
using GestaoBiblioteca.Core.Entities;
using GestaoBiblioteca.Core.Interfaces.Service;
using GestaoBiblioteca.Core.Models.Autor;
using GestaoBiblioteca.Core.Models.Genero;
using GestaoBiblioteca.Core.Models.Livro;
using GestaoBiblioteca.Infrastructure.SqlServer;
using GestaoBiblioteca.Infrastructure.SqlServer.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoBiblioteca.Service
{
    public class GeneroService : BaseRepository<GeneroDTO>, IGeneroService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GeneroService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GeneroDTO> BuscarPorIdAsync(int id)
        {
            try
            {
                var genero = await _context.Generos.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (genero == null)
                    return null;

                var generoDTO = _mapper.Map<GeneroDTO>(genero);

                return generoDTO;
            }

            catch (SqlException ex)
            {
                throw;
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public GeneroDTO BuscarPorId(int id)
        {
            try
            {
                var genero = _context.Generos.Where(x => x.Id == id).FirstOrDefault();

                if (genero == null)
                    return null;

                var generoDTO = _mapper.Map<GeneroDTO>(genero);

                return generoDTO;
            }

            catch (SqlException ex)
            {
                throw;
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public void CorrelacionarLivros(ref IEnumerable<GeneroDTO> entidades)
        {
            try
            {
                if(entidades == null || !entidades.Any())
                    throw new KeyNotFoundException($"É necessário informar um ou mais registros para posterior associação de livros.");

                var livros = _context.Livros.AsNoTracking().ToList();

                foreach (var item in entidades)
                {
                    var livrosGenero = livros.Where(x => x.IdGenero == item.Id).ToList();

                    if (!livrosGenero.Any())
                        continue;
                    else
                        item.Livros = _mapper.Map<ICollection<LivroDTO>>(livrosGenero);
                }
            }

            catch (SqlException ex)
            {
                throw;
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public async override Task<bool> Editar(GeneroDTO entity)
        {
            try
            {
                var autor = _mapper.Map<GeneroDTO>(entity);

                var registro = await _context.Generos.FirstOrDefaultAsync(u => u.Id == entity.Id);

                //Atualiza as propriedades
                if (registro != null)
                    _context.Entry(registro).CurrentValues.SetValues(autor);

                return await _context.SaveChangesAsync() > 0 ? true : false;
            }

            catch (SqlException ex)
            {
                throw;
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public async override Task<bool> ExcluirLogicamente(int id)
        {
            try
            {
                var genero = await _context.Generos.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (genero == null)
                    throw new KeyNotFoundException($"Não foi encontrado registro sob/código {id}.");

                genero.Ativo = false;

                _context.Generos.Update(genero);
                return await _context.SaveChangesAsync() > 0 ? true : false;
            }

            catch (SqlException ex)
            {
                throw;
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public async override Task<bool> ExcluirPermanentemente(int id)
        {
            try
            {
                var genero = await _context.Generos.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (genero == null)
                    throw new KeyNotFoundException($"Não foi encontrado registro sob/código {id}.");

                _context.Generos.Remove(genero);
                return await _context.SaveChangesAsync() > 0 ? true : false;
            }

            catch (SqlException ex)
            {
                throw;
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public async override Task<bool> Incluir(GeneroDTO entity)
        {
            try
            {
                var genero = _mapper.Map<TGeneros>(entity);

                await _context.Generos.AddAsync(genero);
                return await _context.SaveChangesAsync() > 0 ? true : false;
            }

            catch (SqlException ex)
            {
                throw;
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public async override Task<List<GeneroDTO>> ListarTodosAsync()
        {
            try
            {
                var generos = await _context.Autores.AsNoTracking().ToListAsync();

                var generosDTO = _mapper.Map<List<GeneroDTO>>(generos);

                return generosDTO.OrderBy(x => x.Nome).ToList();
            }

            catch (SqlException ex)
            {
                throw;
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public override List<GeneroDTO> ListarTodos()
        {
            try
            {
                var generos = _context.Autores.AsNoTracking().ToList();

                var generosDTO = _mapper.Map<List<GeneroDTO>>(generos);

                return generosDTO.OrderBy(x => x.Nome).ToList();
            }

            catch (SqlException ex)
            {
                throw;
            }

            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
