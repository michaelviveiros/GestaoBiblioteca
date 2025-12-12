using GestaoBiblioteca.Api.Controllers;
using GestaoBiblioteca.Api.Data;
using GestaoBiblioteca.Core.Interfaces.Service;
using GestaoBiblioteca.Core.Models.Autor;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GestaoBiblioteca.Tests.UnitTests.GestaoBiblioteca.Api.Controllers
{
    public class AutorControllerTests : IDisposable
    {
        private readonly Mock<IAutorService> _autorServiceMock;
        private readonly AutorController _controller;

        public AutorControllerTests()
        {
            _autorServiceMock = new Mock<IAutorService>();
            _controller = new AutorController(_autorServiceMock.Object);
        }

        [Fact(DisplayName = "BuscarPorIdAsync - Deve retornar falha quando o id do autor for inválido.")]
        public async Task BuscarPorId_DeveRetornarBadRequest_QuandoIdInvalido()
        {
            //Arrange => Inicialização das variáveis, mocks...
            int idInvalido = 0;

            //Act => Execução do teste, chamada de funções, métodos...
            var request = await _controller.BuscarPorId(idInvalido);

            //Assert => Verificação do resultado da operação anterior.
            var resultResponse = Assert.IsType<BadRequestObjectResult>(request);
            var response = Assert.IsType<ResultResponse<string>>(resultResponse.Value);

            //Assert => Verificação do resultado da operação anterior.
            Assert.IsType<ResultResponse<string>>(response);
            Assert.False(response.Success);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal($"O valor ({idInvalido}) para o campo de Id é inválido.", response.Data);
            Assert.Null(response.Errors);
        }


        [Theory(DisplayName = "BuscarPorId - Deve retornar sucesso quando o autor existir")]
        [InlineData(1)]
        [InlineData(5)]
        public async Task BuscarPorId_DeveRetornarOk_QuandoAutorExistir(int id)
        {
            //Arrange => Inicialização das variáveis, mocks...
            var autor = new AutorDTO
            {
                Id = id,
                Nome = "Machado de Assis",
                Ativo = true
            };

            _autorServiceMock.Setup(x => x.BuscarPorIdAsync(id)).ReturnsAsync(autor);

            //Act => Execução do teste, chamada de funções, métodos...
            var request = await _controller.BuscarPorId(id);

            //Assert => Verificação do resultado da operação anterior.
            var resultResponse = Assert.IsType<OkObjectResult>(request);
            var response = Assert.IsType<ResultResponse<AutorDTO>>(resultResponse.Value);

            Assert.True(response.Success);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Data);
            Assert.Equal(id, response.Data.Id);
            Assert.Equal(autor.Nome, response.Data.Nome);
            Assert.Equal(0, response.Errors.Count);
        }

        public void Dispose()
        {
            //_autorServiceMock = null;
            //_controller = null;
        }
    }
}
