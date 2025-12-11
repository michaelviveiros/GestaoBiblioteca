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
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<LivroService> _logger;

        public LivroService(AppDbContext context, IMapper mapper, ILogger<LivroService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ICollection<LivroDTO>> BuscarPorAutor(int idAutor)
        {
            try
            {
                var livros = await _context.Livros.AsNoTracking().Where(x => x.IdAutor == idAutor).ToListAsync();

                if (!livros.Any())
                    throw new KeyNotFoundException($"Não foram encontrados registros para o autor sob/código: {idAutor}.");

                var livrosDTO = _mapper.Map<ICollection<LivroDTO>>(livros);

                CorrelacionarAutorGenero(livrosDTO);

                return livrosDTO;
            }

            catch (SqlException ex)
            {
                _logger.LogError(ex, "Um erro ocorreu ao buscar o autor do livro sob/código: {idAutor}", idAutor);
                throw;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Um erro ocorreu ao buscar o autor do livro sob/código: {idAutor}", idAutor);
                throw;
            }
        }

        public void CorrelacionarAutorGenero(LivroDTO entidade)
        {
            try
            {
                if (entidade == null)
                    throw new KeyNotFoundException($"É necessário informar um registro para posterior associação de autore e gênero.");

                var genero = _context.Generos.FirstOrDefault(x => x.Id == entidade.IdGenero);
                var autor = _context.Autores.FirstOrDefault(x => x.Id == entidade.IdAutor);

                if (genero != null)
                {
                    var generoDTO = _mapper.Map<GeneroDTO>(genero);
                    entidade.Genero = generoDTO;
                }

                if (autor != null)
                {
                    var autorDTO = _mapper.Map<AutorDTO>(autor);
                    entidade.Autor = autorDTO;
                }
            }

            catch (SqlException ex)
            {
                _logger.LogError(ex, "Um erro ocorreu ao correlacionar autor e gênero para o livro sob/código {LivroId}", entidade.Id);
                throw;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Um erro ocorreu ao correlacionar autor e gênero para o livro sob/código {LivroId}", entidade.Id);
                throw;
            }
        }

        public void CorrelacionarAutorGenero(ICollection<LivroDTO> entidades)
        {
            try
            {
                if (entidades == null || !entidades.Any())
                    throw new KeyNotFoundException($"É necessário informar um ou mais registros para posterior associação de autores e gêneros.");

                var generos = _context.Generos.AsNoTracking().ToList();
                var autores = _context.Autores.AsNoTracking().ToList();

                var generosDTO = _mapper.Map<ICollection<GeneroDTO>>(generos);
                var autoresDTO = _mapper.Map<ICollection<AutorDTO>>(autores);

                foreach (var item in entidades)
                {
                    var genero = generosDTO.Where(x => x.Id == item.IdGenero).FirstOrDefault();
                    var autor = autoresDTO.Where(x => x.Id == item.IdAutor).FirstOrDefault();

                    if (genero != null)
                        item.Genero = genero;

                    if (autor != null)
                        item.Autor = autor;
                }
            }

            catch (SqlException ex)
            {
                _logger.LogError(ex, "Um erro ocorreu ao realizar o correlacionamento das entidades de autor e gênero Entidades {entidades}.", entidades);
                throw;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Um erro ocorreu ao realizar o correlacionamento das entidades de autor e gênero Entidades {entidades}.", entidades);
                throw;
            }
        }

        public async Task<ICollection<LivroDTO>> BuscarPorGenero(int idGenero)
        {
            try
            {
                var generos = await _context.Livros.AsNoTracking().Where(x => x.IdGenero == idGenero).ToListAsync();

                if (!generos.Any())
                    throw new KeyNotFoundException($"Não foram encontrados registros para o gênero sob/código: {idGenero}.");

                var generosDTO = _mapper.Map<ICollection<LivroDTO>>(generos);

                return generosDTO;
            }

            catch (SqlException ex)
            {
                _logger.LogError(ex, "Um erro ocorreu ao buscar o genero do livro sob/código: {idGenero}", idGenero);
                throw;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Um erro ocorreu ao buscar o genero do livro sob/código: {idGenero}", idGenero);
                throw;
            }
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
                _logger.LogError(ex, "Um erro ocorreu ao buscar o livro sob/código: {id}", id);
                throw;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Um erro ocorreu ao buscar o livro sob/código: {id}", id);
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
                _logger.LogError(ex, "Um erro ocorreu ao editar o livro sob/nome: {nomeLivro}", entity.Nome);
                throw;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Um erro ocorreu ao editar o livro sob/nome: {nomeLivro}", entity.Nome);
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
                _logger.LogError(ex, "Um erro ocorreu ao excluir logicamente o livro sob/código: {id}", id);
                throw;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Um erro ocorreu ao excluir logicamente o livro sob/código: {id}", id);
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
                _logger.LogError(ex, "Um erro ocorreu ao excluir permanentemente o livro sob/código: {id}", id);
                throw;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Um erro ocorreu ao excluir permanentemente o livro sob/código: {id}", id);
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
                _logger.LogError(ex, "Um erro ocorreu ao incluir o livro sob/nome: {nome}", entity.Nome);
                throw;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Um erro ocorreu ao incluir o livro sob/nome: {nome}", entity.Nome);
                throw;
            }
        }

        public async override Task<List<LivroDTO>> ListarTodosAsync()
        {
            try
            {
                var livros = await _context.Livros.AsNoTracking().ToListAsync();

                var livrosDTO = _mapper.Map<List<LivroDTO>>(livros);

                return livrosDTO.OrderBy(x => x.Nome).ToList();
            }

            catch (SqlException ex)
            {
                _logger.LogError(ex, "Um erro ocorreu ao realizar a listagem dos livros");
                throw;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Um erro ocorreu ao realizar a listagem dos livros");
                throw;
            }
        }

        public override List<LivroDTO> ListarTodos()
        {
            try
            {
                var livros = _context.Livros.AsNoTracking().ToList();

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
