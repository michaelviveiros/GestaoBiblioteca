using AutoMapper;
using GestaoBiblioteca.Core.Entities;
using GestaoBiblioteca.Core.Interfaces.Service;
using GestaoBiblioteca.Core.Models.Autor;
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
    public class AutorService : BaseRepository<AutorDTO>, IAutorService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AutorService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AutorDTO> BuscarPorIdAsync(int id)
        {
            try
            {
                var autor = await _context.Autores.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (autor == null)
                    return null;

                var autorDTO = _mapper.Map<AutorDTO>(autor);

                return autorDTO;
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


        public AutorDTO BuscarPorId(int id)
        {
            try
            {
                var autor = _context.Autores.Where(x => x.Id == id).FirstOrDefault();

                if (autor == null)
                    return null;

                var autorDTO = _mapper.Map<AutorDTO>(autor);

                return autorDTO;
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

        public void CorrelacionarLivros(ref IEnumerable<AutorDTO> entidades)
        {
            try
            {
                if (entidades == null || !entidades.Any())
                    throw new KeyNotFoundException($"É necessário informar um ou mais registros para posterior associação de livros.");

                var livros = _context.Livros.AsNoTracking().ToList();

                foreach (var item in entidades)
                {
                    var livrosAutor = livros.Where(x => x.IdAutor == item.Id).ToList();

                    if (!livrosAutor.Any())
                        continue;
                    else
                        item.Livros = _mapper.Map<ICollection<LivroDTO>>(livrosAutor);
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

        public async override Task<bool> Editar(AutorDTO entity)
        {
            try
            {
                var autor = _mapper.Map<TAutores>(entity);

                var registro = await _context.Autores.FirstOrDefaultAsync(u => u.Id == entity.Id);

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
                var autor = await _context.Autores.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (autor == null)
                    throw new KeyNotFoundException($"Não foi encontrado registro sob/código {id}.");

                autor.Ativo = false;

                _context.Autores.Update(autor);
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
                var autor = await _context.Autores.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (autor == null)
                    throw new KeyNotFoundException($"Não foi encontrado registro sob/código {id}.");

                _context.Autores.Remove(autor);
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

        public async override Task<bool> Incluir(AutorDTO entity)
        {
            try
            {
                var autor = _mapper.Map<TAutores>(entity);

                _context.Autores.AddAsync(autor);

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

        public async override Task<List<AutorDTO>> ListarTodosAsync()
        {
            try
            {
                var autores = await _context.Autores.AsNoTracking().ToListAsync();

                var autoresDTO = _mapper.Map<List<AutorDTO>>(autores);

                return autoresDTO.OrderBy(x => x.Nome).ToList();
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

        public override List<AutorDTO> ListarTodos()
        {
            try
            {
                var autores = _context.Autores.AsNoTracking().ToList();

                var autoresDTO = _mapper.Map<List<AutorDTO>>(autores);

                return autoresDTO.OrderBy(x => x.Nome).ToList();
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
