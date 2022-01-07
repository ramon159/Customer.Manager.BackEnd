using Moq;
using System;
using System.Threading.Tasks;
using Target.Backend.Web.Interfaces.Repositories;
using Target.Backend.Web.Repositories;
using Xunit;

namespace Target.Backend.Tests.Integration.Repositories
{
    public class EstadoRepositoryTests
    {
        private IEstadoRepository _estadoRepository;

        public EstadoRepositoryTests()
        {
            _estadoRepository = new EstadoRepository();
        }

        [Fact]
        public async Task GetCidadesByUFAsync_UF_ShouldReturnsObject()
        {
            // Arrange
            string UF = "RJ";

            // Act
            var result = await _estadoRepository.GetCidadesByUFAsync(UF);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetEstadosAsync_ShouldReturnsObject()
        {
            // Act
            var result = await _estadoRepository.GetEstadosAsync();

            // Assert
            Assert.NotNull(result);
        }
    }
}
