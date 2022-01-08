using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Target.Backend.Web.Data;
using Target.Backend.Web.Interfaces.Repositories;
using Target.Backend.Web.Models;
using Target.Backend.Web.Repositories;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Target.Backend.Tests.Integration.Repositories
{
    public class ClienteRepositoryTests
    {
        private ApplicationContext _context;
        private IPlanoRepository _planoRepository;
        private IClienteRepository _clienteRepository;

        public ClienteRepositoryTests()
        {
            DbContextOptionsBuilder<ApplicationContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()); // todos os testes usarão um db diferente

            _context = new ApplicationContext(optionsBuilder.Options);

            _planoRepository = new PlanoRepository(_context);
            _clienteRepository = new ClienteRepository(_context, _planoRepository);
        }

        [Fact]
        public async Task GetClienteByID_ClienteId_ShouldReturnClienteByID()
        {
            // Arrange
            Cliente cliente = new Cliente()
            {
                NomeCompleto = $"Cliente",
                CPF = "39729855099",
                RendaMensal = 7000,
                DataNascimento = DateTime.Today
            };
            // Act
            _clienteRepository.InsertCliente(cliente);
            _context.SaveChanges();
            var result = await _clienteRepository.GetClienteByID(cliente.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result.Id, cliente.Id);
        }

        [Fact]
        public async Task GetClientes_SortOrderIsFim_ShouldReturnsClienteList()
        {
            // Arrange
            string sortOrder = "fim";
            for (int i = 1; i <= 10; i++)
            {
                Cliente cliente = new Cliente()
                {
                    NomeCompleto = $"Cliente {i}",
                    CPF = "39729855099",
                    RendaMensal = 7000,
                    DataNascimento = DateTime.Today
                };
                _clienteRepository.InsertCliente(cliente);
            }
            _context.SaveChanges();

            // Act
            var result = await _clienteRepository.GetClientes(sortOrder);
            int resultCount = result.Count();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<Cliente>>(result);
            Assert.Equal(10, resultCount);
        }

        [Fact]
        public async Task GetClientesByRenda_Renda_ShouldReturnClienteListWhereRendaMensalIsGreaterThanRenda()
        {
            // Arrange
            decimal rendaMensal = 6000;
            for (int i = 1; i <= 4; i++)
            {
                Cliente cliente = new Cliente()
                {
                    NomeCompleto = $"Cliente {i}",
                    CPF = "39729855099",
                    RendaMensal = 7000,
                    DataNascimento = DateTime.Today
                };
                _clienteRepository.InsertCliente(cliente);
            }            
            for (int i = 1; i <= 5; i++)
            {
                Cliente cliente = new Cliente()
                {
                    NomeCompleto = $"Cliente {i}",
                    CPF = "39729855099",
                    RendaMensal = 5000,
                    DataNascimento = DateTime.Today
                };
                _clienteRepository.InsertCliente(cliente);
            }
            _context.SaveChanges();

            // Act
            var result = await _clienteRepository.GetClientesByRenda(rendaMensal);
            int resultCount = result.Count();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(4, resultCount);
            Assert.Contains(result, item => item.RendaMensal >= rendaMensal);
        }

        [Fact]
        public void InsertCliente_Cliente_ShouldPersistSingleCliente()
        {
            // Arrange
            Cliente cliente = new Cliente()
            {
                NomeCompleto = $"Cliente",
                CPF = "39729855099",
                RendaMensal = 7000,
                DataNascimento = DateTime.Today
            };

            // Act
            _clienteRepository.InsertCliente(cliente);
            int result = _context.SaveChanges();
            List<Cliente> clientes = _context.Cliente.ToList();

            // Assert
            Assert.True(Convert.ToBoolean(result));
            Assert.Single(clientes);
        }

        [Fact]
        public async void UpdatePlanoCliente_Cliente_ClienteMustHasPlan()
        {
            // Arrange
            Plano plano = new Plano()
            {
                Id = 1,
                Titulo = "Plano VIP",
                Descricao = "Descricao",
                Preco = 50.00m,
                CriadoEm = DateTime.UtcNow
            };

            Cliente cliente = new Cliente()
            {
                NomeCompleto = $"Cliente",
                CPF = "39729855099",
                RendaMensal = 7000,
                DataNascimento = DateTime.Today
            };

            _context.Plano.Add(plano);
            _clienteRepository.InsertCliente(cliente);
            _context.SaveChanges();


            // Act
            _clienteRepository.UpdatePlanoCliente(cliente);
            _context.SaveChanges();
            var result = await _clienteRepository.GetClienteByID(cliente.Id);

            // Assert
            Assert.NotNull(result.Plano);
        }
    }
}
