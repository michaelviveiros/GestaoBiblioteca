using GestaoBiblioteca.Api.Data;
using GestaoBiblioteca.Core.Interfaces.Service;
using GestaoBiblioteca.Core.Models.Autor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Net;

namespace GestaoBiblioteca.Api.Controllers
{
    public class AutorController : Controller
    {
        private IAutorService _autorService;

        public AutorController(IAutorService autorService)
        {
            _autorService = autorService;
        }

        /// <summary>
        /// Realiza a busca de um determinado autor pelo seu codigo de registro.
        /// </summary>
        /// <param name="id">Código de registro do autor</param>
        /// <returns>Retorna a entidade do Autor</returns>
        [HttpGet("/BuscarPorId/{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new ResultResponse<string>
                        (data: $"O valor ({id}) para o campo de Id é inválido."));

                var resultado = await _autorService.BuscarPorId(id);

                if (resultado == null)
                    return NotFound(new ResultResponse<string>
                        (data: $"Não foi localizado nenhum autor sob/código: {id}."));

                return Ok(new ResultResponse<AutorDTO>
                (
                    success: true,
                    statusCode: HttpStatusCode.OK,
                    data: resultado
                ));
            }

            catch(Exception ex)
            {
                return BadRequest(new ResultResponse<string>
                (
                    data: $"Um erro ocorreu ao executar o método {nameof(BuscarPorId)} na classe {nameof(AutorController)}.\r\n" +
                          $"Detalhes Técnicos: {ex.Message}")
                );
            }
        }

        /// <summary>
        /// Realiza a ctiação de um novo autor na base de dados.
        /// </summary>
        /// <param name="entity">Entidade Autor</param>
        /// <returns></returns>
        [HttpPost("/Criar")]
        public async Task<IActionResult> Criar([FromBody] AutorDTO entity)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResultResponse<string>(
                        data: "Todos os campos do Autor são de preenchimento obrigatório."));

                var resultado = await _autorService.Incluir(entity);

                if (resultado == false)
                    return BadRequest(new ResultResponse<string>(
                        data: "Um erro ocorreu ao validar a atualização internamente. Consulte os logs para mais detalhes do erro."));

                return Ok(new ResultResponse<string>
                (
                    success: true,
                    statusCode: HttpStatusCode.OK,
                    data: $"O autor ({entity.Nome}) foi inserido com sucesso."
                ));
            }

            catch(Exception ex)
            {
                return BadRequest(new ResultResponse<string>
                (
                    data: $"Um erro ocorreu ao executar o método {nameof(BuscarPorId)} na classe {nameof(AutorController)}.\r\n" +
                          $"Detalhes Técnicos: {ex.Message}")
                );
            }
        }

        /// <summary>
        /// Realiza a edição de um editor na base de dados.
        /// </summary>
        /// <param name="entity">Entidade do Autor</param>
        /// <returns></returns>
        [HttpPut("/Editar/{id}")]
        public async Task<IActionResult> Editar([FromBody] AutorDTO entity)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResultResponse<string>(
                        data: "Todos os campos do Autor são de preenchimento obrigatório."));

                var resultado = await _autorService.Editar(entity);

                if (!resultado)
                    return BadRequest(new ResultResponse<string>(
                        data: "Não foi possível atualizar o autor. Verifique se o ID informado existe."));

                return Ok(new ResultResponse<string>
                (
                    success: true,
                    statusCode: HttpStatusCode.OK,
                    data: $"Os dados do autor ({entity.Nome}) foram atualizados com sucesso."
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultResponse<string>(
                    data: $"Um erro ocorreu ao executar o método {nameof(Editar)} na classe {nameof(AutorController)}.\r\n" +
                          $"Detalhes Técnicos: {ex.Message}"
                ));
            }
        }

        /// <summary>
        /// Realiza a exclusão lógica de um autor na base de dados.
        /// </summary>
        /// <param name="id">Código do Autor</param>
        /// <returns></returns>
        [HttpDelete("/ExcluirLogicamente/{id}")]
        public async Task<IActionResult> ExcluirLogicamente(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new ResultResponse<string>(
                        data: $"O valor ({id}) para o campo Id é inválido."));

                var resultado = await _autorService.ExcluirLogicamente(id);

                if (!resultado)
                    return NotFound(new ResultResponse<string>(
                        data: $"Não foi encontrado nenhum autor com o código ({id})."
                    ));

                return Ok(new ResultResponse<string>
                (
                    success: true,
                    statusCode: HttpStatusCode.OK,
                    data: $"O autor (ID {id}) foi excluído com sucesso."
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultResponse<string>(
                    data: $"Um erro ocorreu ao executar o método {nameof(ExcluirLogicamente)} na classe {nameof(AutorController)}.\r\n" +
                          $"Detalhes Técnicos: {ex.Message}"
                ));
            }
        }

        /// <summary>
        /// Realiza a exclusão permanente de um autor na base de dados.
        /// </summary>
        /// <param name="id">Código do Autor</param>
        /// <returns></returns>
        [HttpDelete("/ExcluirPermanentemente/{id}")]
        public async Task<IActionResult> ExcluirPermanentemente(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new ResultResponse<string>(
                        data: $"O valor ({id}) para o campo Id é inválido."));

                var resultado = await _autorService.ExcluirPermanentemente(id);

                if (!resultado)
                    return NotFound(new ResultResponse<string>(
                        data: $"Não foi encontrado nenhum autor com o código ({id})."
                    ));

                return Ok(new ResultResponse<string>
                (
                    success: true,
                    statusCode: HttpStatusCode.OK,
                    data: $"O autor (ID {id}) foi excluído com sucesso."
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultResponse<string>(
                    data: $"Um erro ocorreu ao executar o método {nameof(ExcluirPermanentemente)} na classe {nameof(AutorController)}.\r\n" +
                          $"Detalhes Técnicos: {ex.Message}"
                ));
            }
        }

        /// <summary>
        /// Realiza a listagem de todos os autores na base de dados.
        /// </summary>
        /// <returns></returns>
        [HttpGet("/Listar")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var resultado = await _autorService.ListarTodos();

                if (resultado == null || !resultado.Any())
                    return NotFound(new ResultResponse<string>(
                        data: "Não foram encontrados regiostros de autores na base de dados."));

                return Ok(new ResultResponse<IEnumerable<AutorDTO>>
                (
                    success: true,
                    statusCode: HttpStatusCode.OK,
                    data: resultado
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultResponse<string>(
                    data: $"Um erro ocorreu ao executar o método {nameof(Listar)} na classe {nameof(AutorController)}.\r\n" +
                          $"Detalhes Técnicos: {ex.Message}"
                ));
            }
        }
    }
}
