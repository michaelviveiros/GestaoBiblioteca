using GestaoBiblioteca.Api.Data;
using GestaoBiblioteca.Core.Interfaces.Service;
using GestaoBiblioteca.Core.Models.Autor;
using GestaoBiblioteca.Core.Models.Livro;
using GestaoBiblioteca.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GestaoBiblioteca.Api.Controllers
{
    public class LivroController : Controller
    {
        private ILivroService _livroService;

        public LivroController(ILivroService livroService)
        {
            _livroService = livroService;
        }

        /// <summary>
        /// Realiza a busca de um determinado livro pelo seu codigo de registro.
        /// </summary>
        /// <param name="id">Código de registro do livro</param>
        /// <returns>Retorna a entidade do Autor</returns>
        [HttpGet("/Livro/BuscarPorId/{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new ResultResponse<string>
                        (data: $"O valor ({id}) para o campo de Id é inválido."));

                var resultado = await _livroService.BuscarPorId(id);

                if (resultado == null)
                    return NotFound(new ResultResponse<string>
                        (data: $"Não foi localizado nenhum livro sob/código: {id}."));

                return Ok(new ResultResponse<LivroDTO>
                (
                    success: true,
                    statusCode: HttpStatusCode.OK,
                    data: resultado
                ));
            }

            catch (Exception ex)
            {
                return BadRequest(new ResultResponse<string>
                (
                    data: $"Um erro ocorreu ao executar o método {nameof(BuscarPorId)} na classe {nameof(LivroController)}.\r\n" +
                          $"Detalhes Técnicos: {ex.Message}")
                );
            }
        }

        /// <summary>
        /// Realiza a ctiação de um novo livro na base de dados.
        /// </summary>
        /// <param name="entity">Entidade Livro</param>
        /// <returns></returns>
        [HttpPost("/Livro/Criar")]
        public async Task<IActionResult> Criar([FromBody] LivroDTO entity)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResultResponse<string>(
                        data: "Todos os campos do Livro são de preenchimento obrigatório."));

                var resultado = await _livroService.Incluir(entity);

                if (resultado == false)
                    return BadRequest(new ResultResponse<string>(
                        data: "Um erro ocorreu ao validar a atualização internamente. Consulte os logs para mais detalhes do erro."));

                return Ok(new ResultResponse<string>
                (
                    success: true,
                    statusCode: HttpStatusCode.OK,
                    data: $"O livro ({entity.Nome}) foi inserido com sucesso."
                ));
            }

            catch (Exception ex)
            {
                return BadRequest(new ResultResponse<string>
                (
                    data: $"Um erro ocorreu ao executar o método {nameof(BuscarPorId)} na classe {nameof(LivroController)}.\r\n" +
                          $"Detalhes Técnicos: {ex.Message}")
                );
            }
        }

        /// <summary>
        /// Realiza a edição de um livro na base de dados.
        /// </summary>
        /// <param name="entity">Entidade do Livro</param>
        /// <returns></returns>
        [HttpPut("/Livro/Editar/{id}")]
        public async Task<IActionResult> Editar([FromBody] LivroDTO entity)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResultResponse<string>(
                        data: "Todos os campos do Livro são de preenchimento obrigatório."));

                var resultado = await _livroService.Editar(entity);

                if (!resultado)
                    return BadRequest(new ResultResponse<string>(
                        data: "Não foi possível atualizar o livro. Verifique se o ID informado existe."));

                return Ok(new ResultResponse<string>
                (
                    success: true,
                    statusCode: HttpStatusCode.OK,
                    data: $"Os dados do livro ({entity.Nome}) foram atualizados com sucesso."
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultResponse<string>(
                    data: $"Um erro ocorreu ao executar o método {nameof(Editar)} na classe {nameof(LivroController)}.\r\n" +
                          $"Detalhes Técnicos: {ex.Message}"
                ));
            }
        }

        /// <summary>
        /// Realiza a exclusão lógica de um livro na base de dados.
        /// </summary>
        /// <param name="id">Código do Livro</param>
        /// <returns></returns>
        [HttpDelete("/Livro/ExcluirLogicamente/{id}")]
        public async Task<IActionResult> ExcluirLogicamente(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new ResultResponse<string>(
                        data: $"O valor ({id}) para o campo Id é inválido."));

                var resultado = await _livroService.ExcluirLogicamente(id);

                if (!resultado)
                    return NotFound(new ResultResponse<string>(
                        data: $"Não foi encontrado nenhum livro com o código ({id})."
                    ));

                return Ok(new ResultResponse<string>
                (
                    success: true,
                    statusCode: HttpStatusCode.OK,
                    data: $"O livro sob/código: ({id}) foi excluído com sucesso."
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultResponse<string>(
                    data: $"Um erro ocorreu ao executar o método {nameof(ExcluirLogicamente)} na classe {nameof(LivroController)}.\r\n" +
                          $"Detalhes Técnicos: {ex.Message}"
                ));
            }
        }

        /// <summary>
        /// Realiza a exclusão permanente de um livro na base de dados.
        /// </summary>
        /// <param name="id">Código do Livro</param>
        /// <returns></returns>
        [HttpDelete("/Livro/ExcluirPermanentemente/{id}")]
        public async Task<IActionResult> ExcluirPermanentemente(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new ResultResponse<string>(
                        data: $"O valor ({id}) para o campo Id é inválido."));

                var resultado = await _livroService.ExcluirPermanentemente(id);

                if (!resultado)
                    return NotFound(new ResultResponse<string>(
                        data: $"Não foi encontrado nenhum livro com o código ({id})."
                    ));

                return Ok(new ResultResponse<string>
                (
                    success: true,
                    statusCode: HttpStatusCode.OK,
                    data: $"O livro sob/código: ({id}) foi excluído com sucesso."
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultResponse<string>(
                    data: $"Um erro ocorreu ao executar o método {nameof(ExcluirPermanentemente)} na classe {nameof(LivroController)}.\r\n" +
                          $"Detalhes Técnicos: {ex.Message}"
                ));
            }
        }

        /// <summary>
        /// Realiza a listagem de todos os livros na base de dados.
        /// </summary>
        /// <returns></returns>
        [HttpGet("/Livro/Listar")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var resultado = await _livroService.ListarTodosAsync();

                if (resultado == null || !resultado.Any())
                    return NotFound(new ResultResponse<string>(
                        data: "Não foram encontrados regiostros de livros na base de dados."));

                return Ok(new ResultResponse<IEnumerable<LivroDTO>>
                (
                    success: true,
                    statusCode: HttpStatusCode.OK,
                    data: resultado
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultResponse<string>(
                    data: $"Um erro ocorreu ao executar o método {nameof(Listar)} na classe {nameof(LivroController)}.\r\n" +
                          $"Detalhes Técnicos: {ex.Message}"
                ));
            }
        }
    }
}
