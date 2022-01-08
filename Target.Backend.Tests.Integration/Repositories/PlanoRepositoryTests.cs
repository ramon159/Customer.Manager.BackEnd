using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Threading.Tasks;
using Target.Backend.Web.Data;
using Target.Backend.Web.Interfaces.Repositories;
using Target.Backend.Web.Models;
using Target.Backend.Web.Repositories;
using Xunit;

namespace Target.Backend.Tests.Integration.Repositories
{
    public class PlanoRepositoryTests
    {
        private readonly ApplicationContext _context;
        private readonly IPlanoRepository _planoRepository;
        private readonly IClienteRepository _clienteRepository;
        public PlanoRepositoryTests()
        {
            DbContextOptionsBuilder<ApplicationContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()); // todos os testes usarão um db diferente

            _context = new ApplicationContext(optionsBuilder.Options);
            _planoRepository = new PlanoRepository(_context);
            _clienteRepository = new ClienteRepository(_context, _planoRepository);
        }



        [Fact]
        public async Task GetPlanoByID_PlanoId_ShouldReturnPlano()
        {
            // Arrange
            Plano plano = new Plano()
            {
                Titulo = "Plano VIP",
                Descricao = "Descricao",
                Preco = 50.00m,
                CriadoEm = DateTime.UtcNow
            };

            _context.Plano.Add(plano);
            _context.SaveChanges();

            // Act
            var result = await _planoRepository.GetPlanoByID(plano.Id);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<Plano>(result);
        }

        [Fact]
        public async Task GetIndiceAdesaoPlano_ShouldReturnIndiceEquals10()
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

            _context.Plano.Add(plano);

            for(int i = 1;i <= 10; i++)
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
            var result = await _planoRepository.GetIndiceAdesaoPlano();

            // Assert
            Assert.Equal(10, result);
        }
    }
}
