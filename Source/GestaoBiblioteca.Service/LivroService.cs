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
    public class LivroService : BaseRepository<LivroDTO>, ILivroService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public LivroService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LivroDTO> BuscarPorId(int id)
        {
            try
            {
                var livro = await _context.Livros.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (livro == null)
                    return null;

                var livroDTO = _mapper.Map<LivroDTO>(livro);

                return livroDTO;
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

        public async override Task<bool> Editar(LivroDTO entity)
        {
            try
            {
                var livro = _mapper.Map<TLivros>(entity);

                var registro = await _context.Livros.FirstOrDefaultAsync(u => u.Id == entity.Id);

                //Atualiza as propriedades
                if (registro != null)
                    _context.Entry(registro).CurrentValues.SetValues(livro);

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
                var livro = await _context.Livros.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (livro == null)
                    throw new KeyNotFoundException($"Não foi encontrado registro sob/código {id}.");

                livro.Ativo = false;

                _context.Livros.Update(livro);
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
                var livro = await _context.Livros.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (livro == null)
                    throw new KeyNotFoundException($"Não foi encontrado registro sob/código {id}.");

                _context.Livros.Remove(livro);
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

        public async override Task<bool> Incluir(LivroDTO entity)
        {
            try
            {
                var livro = _mapper.Map<TLivros>(entity);

                await _context.Livros.AddAsync(livro);
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

        public async override Task<List<LivroDTO>> ListarTodos()
        {
            try
            {
                var livros = await _context.Livros.AsNoTracking().ToListAsync();

                var livrosDTO = _mapper.Map<List<LivroDTO>>(livros);

                return livrosDTO.OrderBy(x => x.Nome).ToList();
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
