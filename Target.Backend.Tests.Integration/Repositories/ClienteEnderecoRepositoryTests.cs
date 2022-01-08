using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Target.Backend.Web.Data;
using Target.Backend.Web.Interfaces.Repositories;
using Target.Backend.Web.Models;
using Target.Backend.Web.Repositories;
using Xunit;

namespace Target.Backend.Tests.Integration.Repositories
{
    public class ClienteEnderecoRepositoryTests
    {
        private ApplicationContext _context;
        private IClienteEnderecoRepository _clienteEnderecoRepository;

        public ClienteEnderecoRepositoryTests()
        {
            DbContextOptionsBuilder<ApplicationContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()); // todos os testes usarão um db diferente

            _context = new ApplicationContext(optionsBuilder.Options);
            _clienteEnderecoRepository = new ClienteEnderecoRepository(_context);

        }
        [Fact]
        public async Task GetClienteEnderecoById_ClienteId_ShouldReturnsClienteById()
        {
            // Arrange
            Cliente cliente = new Cliente()
            {
                NomeCompleto = $"Cliente",
                CPF = "39729855099",
                RendaMensal = 7000,
                DataNascimento = DateTime.Today
            };
            ClienteEndereco endereco = new ClienteEndereco()
            {
                Logradouro = "string",
                Bairro = "string",
                Cidade = "string",
                Complemento = "string",
                UF = "RJ",
                CEP = "72505101",
                Cliente = cliente,
            };

            _context.Cliente.Add(cliente);
            _context.ClienteEndereco.Add(endereco);
            _context.SaveChanges();

            // Act
            var result = await _clienteEnderecoRepository.GetClienteEnderecoById(cliente.Id);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void InsertClienteEndereco_Endereco_EnderecoMustPersist()
        {
            // Arrange
            Cliente cliente = new Cliente()
            {
                NomeCompleto = $"Cliente",
                CPF = "39729855099",
                RendaMensal = 7000,
                DataNascimento = DateTime.Today
            };
            ClienteEndereco endereco = new ClienteEndereco()
            {
                Logradouro = "string",
                Bairro = "string",
                Cidade = "string",
                Complemento = "string",
                UF = "RJ",
                CEP = "72505101",
            };


            // Act
            _context.Cliente.Add(cliente);
            _clienteEnderecoRepository.InsertClienteEndereco(endereco);
            _context.SaveChanges();
            var result = await _context.ClienteEndereco.ToListAsync();
            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
        }
    }
}
