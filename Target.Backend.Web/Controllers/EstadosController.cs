using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Target.Backend.Web.Attributes;
using Target.Backend.Web.Interfaces.Repositories;
using Target.Backend.Web.Interfaces.Services;

namespace Target.Backend.Web.Controllers
{
    [Route("api/v1/estados")]
    [ApiController]
    [Produces("application/json")]
    [ApiKey]
    public class EstadosController : ControllerBase
    {
        private readonly IEstadoRepository _estadoRepository;
        private readonly ILoggerManager _logger;
        public EstadosController(IEstadoRepository estadoRepository, ILoggerManager logger)
        {
            _estadoRepository = estadoRepository;
            _logger = logger;
        }
        /// <summary>
        /// GET: api/v1/estados
        /// </summary>
        /// <returns>todos os estados brasileiros</returns>
        [HttpGet]
        public async Task<ActionResult> GetEstados()
        {
            _logger.LogInfo($"Buscando dados de todos os estados brasileiros na api do IBGE");
            var estados = await _estadoRepository.GetEstadosAsync();
            _logger.LogInfo("Busca finalizada");
            return Ok(estados);
        }
        /// <summary>
        /// GET: api/v1/{UF}/cidades
        /// </summary>
        /// <returns>cidades pela UF informada</returns>
        [HttpGet("{UF}/cidades")]
        public async Task<ActionResult> GetCidadesByUF(string UF)
        {
            _logger.LogInfo($"Buscando dados de todos as cidades do estado de {UF} na api do IBGE");
            var estados = await _estadoRepository.GetCidadesByUFAsync(UF);
            _logger.LogInfo("Busca finalizada");
            return Ok(estados);
        }
    }
}
