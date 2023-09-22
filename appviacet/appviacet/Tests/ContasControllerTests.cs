using appviacet.Context;
using appviacet.Context.Entitys;
using appviacet.Controllers;
using appviacet.Services.Internal;
using appviacet.Services.Internal.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appviacet.Tests
{
    [TestFixture]
    public class ContasControllerTests
    {
        private AppViaCetContext _context;
        private ContasService _contasService;

        public ContasControllerTests( ) 
        {
           
        }

        

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppViaCetContext>()
                .UseInMemoryDatabase("InMemoryTestDatabase")
                .Options;

            _context = new AppViaCetContext(options);
            _contasService = new ContasService(_context);
        }

        [Test]
        public async Task GetContas_DeveRetornarOkComContas()
        {
            
            var contasServiceMock = new Mock<ContasService>();
            contasServiceMock.Setup(service => service.GetContasAsync())
                .ReturnsAsync(new List<Conta>
                {
            new Conta { Id = 1, Nome = "Conta1", Descricao = "Descrição1" },
            new Conta { Id = 2, Nome = "Conta2", Descricao = "Descrição2" }
                });

            var controller = new ContasController(contasServiceMock.Object);

            
            var result = await controller.GetContas();

            
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var contas = okResult.Value as IEnumerable<Conta>;
            Assert.IsNotNull(contas);
            Assert.AreEqual(2, contas.Count());
        }


        [Test]
        public async Task GetConta_DeveRetornarOkComContaExistente()
        {
            var contasServiceMock = new Mock<ContasService>();
            var idContaExistente = 1;
            contasServiceMock.Setup(service => service.GetContaAsync(idContaExistente))
                .ReturnsAsync(new Conta { Id = idContaExistente, Nome = "Conta1", Descricao = "Descrição1" });

            var controller = new ContasController(contasServiceMock.Object);

            var result = await controller.GetConta(idContaExistente);

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var conta = okResult.Value as Conta;
            Assert.IsNotNull(conta);
            Assert.AreEqual(idContaExistente, conta.Id);
        }

        [Test]
        public async Task GetConta_DeveRetornarNotFoundQuandoContaNaoExistir()
        {
            var contasServiceMock = new Mock<ContasService>();
            var idContaNaoExistente = 999;
            contasServiceMock.Setup(service => service.GetContaAsync(idContaNaoExistente))
                .ReturnsAsync((Conta)null);

            var controller = new ContasController((ContasService)contasServiceMock.Object);

            var result = await controller.GetConta(idContaNaoExistente);

            var notFoundResult = result.Result as NotFoundResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [Test]
        public async Task PostConta_DeveRetornarCreatedAtAction()
        {
            var contasServiceMock = new Mock<ContasService>();
            var novaConta = new Conta { Nome = "NovaConta", Descricao = "Descrição da Nova Conta" };
            contasServiceMock.Setup(service => service.CreateContaAsync(novaConta))
                .ReturnsAsync(new Conta { Id = 1, Nome = novaConta.Nome, Descricao = novaConta.Descricao });

            var controller = new ContasController(contasServiceMock.Object);

            var result = await controller.PostConta(novaConta);

            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);
            Assert.AreEqual(nameof(controller.GetConta), createdAtActionResult.ActionName);
            Assert.AreEqual(1, createdAtActionResult.RouteValues["id"]);
        }

        [Test]
        public async Task PutConta_DeveRetornarOk()
        {
            var contasServiceMock = new Mock<ContasService>();
            var idContaExistente = 1;
            var contaAtualizada = new Conta { Id = idContaExistente, Nome = "ContaAtualizada", Descricao = "Descrição Atualizada" };
            contasServiceMock.Setup(service => service.UpdateContaAsync(idContaExistente, contaAtualizada))
                .ReturnsAsync(contaAtualizada);

            var controller = new ContasController(contasServiceMock.Object);

            var result = await controller.PutConta(idContaExistente, contaAtualizada);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(contaAtualizada, okResult.Value);
        }

        [Test]
        public async Task PutConta_DeveRetornarBadRequestQuandoIdNaoEncontrado()
        {
            var contasServiceMock = new Mock<ContasService>();
            var idContaNaoExistente = 999;
            var contaAtualizada = new Conta { Id = idContaNaoExistente, Nome = "ContaAtualizada", Descricao = "Descrição Atualizada" };
            contasServiceMock.Setup(service => service.UpdateContaAsync(idContaNaoExistente, contaAtualizada))
                .ThrowsAsync(new ArgumentException("Id não encontrado"));

            var controller = new ContasController(contasServiceMock.Object);

            var result = await controller.PutConta(idContaNaoExistente, contaAtualizada);

            
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("Id nao encontrado", badRequestResult.Value);
        }
    }
}
