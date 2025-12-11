using GestaoBiblioteca.Api.Data;
using GestaoBiblioteca.Core.Interfaces.Service;
using GestaoBiblioteca.Core.Models.Autor;
using GestaoBiblioteca.Core.Models.Genero;
using GestaoBiblioteca.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GestaoBiblioteca.Api.Controllers
{
    public class GeneroController : Controller
    {
        private IGeneroService _generoService;

        public GeneroController(IGeneroService generoService)
        {
            _generoService = generoService;
        }

        /// <summary>
        /// Realiza a busca de um determinado genero pelo seu codigo de registro.
        /// </summary>
        /// <param name="id">Código de registro do genero</param>
        /// <returns>Retorna a entidade do Genero</returns>
        [HttpGet("/Genero/BuscarPorId/{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new ResultResponse<string>
                        (data: $"O valor ({id}) para o campo de Id é inválido."));

                var resultado = await _generoService.BuscarPorIdAsync(id);

                if (resultado == null)
                    return NotFound(new ResultResponse<string>
                        (data: $"Não foi localizado nenhum gênero sob/código: {id}."));

                return Ok(new ResultResponse<GeneroDTO>
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
                    data: $"Um erro ocorreu ao executar o método {nameof(BuscarPorId)} na classe {nameof(GeneroController)}.\r\n" +
                          $"Detalhes Técnicos: {ex.Message}")
                );
            }
        }

        /// <summary>
        /// Realiza a ctiação de um novo genero na base de dados.
        /// </summary>
        /// <param name="entity">Entidade Genero</param>
        /// <returns></returns>
        [HttpPost("/Genero/Criar")]
        public async Task<IActionResult> Criar([FromBody] GeneroDTO entity)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResultResponse<string>(
                        data: "Todos os campos do Autor são de preenchimento obrigatório."));

                var resultado = await _generoService.Incluir(entity);

                if (resultado == false)
                    return BadRequest(new ResultResponse<string>(
                        data: "Um erro ocorreu ao validar a atualização internamente. Consulte os logs para mais detalhes do erro."));

                return Ok(new ResultResponse<string>
                (
                    success: true,
                    statusCode: HttpStatusCode.OK,
                    data: $"O genero ({entity.Nome}) foi inserido com sucesso."
                ));
            }

            catch (Exception ex)
            {
                return BadRequest(new ResultResponse<string>
                (
                    data: $"Um erro ocorreu ao executar o método {nameof(BuscarPorId)} na classe {nameof(GeneroController)}.\r\n" +
                          $"Detalhes Técnicos: {ex.Message}")
                );
            }
        }

        /// <summary>
        /// Realiza a edição de um genero na base de dados.
        /// </summary>
        /// <param name="entity">Entidade do Genero</param>
        /// <returns></returns>
        [HttpPut("/Genero/Editar/{id}")]
        public async Task<IActionResult> Editar([FromBody] GeneroDTO entity)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResultResponse<string>(
                        data: "Todos os campos do Autor são de preenchimento obrigatório."));

                var resultado = await _generoService.Editar(entity);

                if (!resultado)
                    return BadRequest(new ResultResponse<string>(
                        data: "Não foi possível atualizar o genero. Verifique se o id informado existe."));

                return Ok(new ResultResponse<string>
                (
                    success: true,
                    statusCode: HttpStatusCode.OK,
                    data: $"Os dados do genero ({entity.Nome}) foram atualizados com sucesso."
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultResponse<string>(
                    data: $"Um erro ocorreu ao executar o método {nameof(Editar)} na classe {nameof(GeneroController)}.\r\n" +
                          $"Detalhes Técnicos: {ex.Message}"
                ));
            }
        }

        /// <summary>
        /// Realiza a exclusão lógica de um genero na base de dados.
        /// </summary>
        /// <param name="id">Código do Genero</param>
        /// <returns></returns>
        [HttpDelete("/Genero/ExcluirLogicamente/{id}")]
        public async Task<IActionResult> ExcluirLogicamente(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new ResultResponse<string>(
                        data: $"O valor ({id}) para o campo Id é inválido."));

                var resultado = await _generoService.ExcluirLogicamente(id);

                if (!resultado)
                    return NotFound(new ResultResponse<string>(
                        data: $"Não foi encontrado nenhum autor com o código ({id})."
                    ));

                return Ok(new ResultResponse<string>
                (
                    success: true,
                    statusCode: HttpStatusCode.OK,
                    data: $"O genero sob/código: ({id}) foi excluído com sucesso."
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultResponse<string>(
                    data: $"Um erro ocorreu ao executar o método {nameof(ExcluirLogicamente)} na classe {nameof(GeneroController)}.\r\n" +
                          $"Detalhes Técnicos: {ex.Message}"
                ));
            }
        }

        /// <summary>
        /// Realiza a exclusão permanente de um genero na base de dados.
        /// </summary>
        /// <param name="id">Código do Genero</param>
        /// <returns></returns>
        [HttpDelete("/Genero/ExcluirPermanentemente/{id}")]
        public async Task<IActionResult> ExcluirPermanentemente(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new ResultResponse<string>(
                        data: $"O valor ({id}) para o campo Id é inválido."));

                var resultado = await _generoService.ExcluirPermanentemente(id);

                if (!resultado)
                    return NotFound(new ResultResponse<string>(
                        data: $"Não foi encontrado nenhum genero com o código ({id})."
                    ));

                return Ok(new ResultResponse<string>
                (
                    success: true,
                    statusCode: HttpStatusCode.OK,
                    data: $"O genero sob/código: ({id}) foi excluído com sucesso."
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultResponse<string>(
                    data: $"Um erro ocorreu ao executar o método {nameof(ExcluirPermanentemente)} na classe {nameof(GeneroController)}.\r\n" +
                          $"Detalhes Técnicos: {ex.Message}"
                ));
            }
        }

        /// <summary>
        /// Realiza a listagem de todos os generos na base de dados.
        /// </summary>
        /// <returns></returns>
        [HttpGet("/Genero/Listar")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var resultado = await _generoService.ListarTodosAsync();

                if (resultado == null || !resultado.Any())
                    return NotFound(new ResultResponse<string>(
                        data: "Não foram encontrados regiostros de generos na base de dados."));

                return Ok(new ResultResponse<IEnumerable<GeneroDTO>>
                (
                    success: true,
                    statusCode: HttpStatusCode.OK,
                    data: resultado
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultResponse<string>(
                    data: $"Um erro ocorreu ao executar o método {nameof(Listar)} na classe {nameof(GeneroController)}.\r\n" +
                          $"Detalhes Técnicos: {ex.Message}"
                ));
            }
        }
    }
}
