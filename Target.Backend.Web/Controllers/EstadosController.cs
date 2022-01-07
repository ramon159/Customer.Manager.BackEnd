using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Target.Backend.Web.Attributes;
using Target.Backend.Web.Interfaces.Repositories;

namespace Target.Backend.Web.Controllers
{
    [Route("api/v1/estados")]
    [ApiController]
    [Produces("application/json")]
    [ApiKey]
    public class EstadosController : ControllerBase
    {
        IEstadoRepository _estadoRepository;

        public EstadosController(IEstadoRepository estadoRepository)
        {
            _estadoRepository = estadoRepository;
        }
        /// <summary>
        /// GET: api/v1/estados
        /// </summary>
        /// <returns>todos os estados brasileiros</returns>
        [HttpGet]
        public async Task<ActionResult> GetEstados()
        {
            var estados = await _estadoRepository.GetEstadosAsync();
            return Ok(estados);
        }
        /// <summary>
        /// GET: api/v1/{UF}/cidades
        /// </summary>
        /// <returns>cidades pela UF informada</returns>
        [HttpGet("{UF}/cidades")]
        public async Task<ActionResult> GetCidadesByUF(string UF)
        {
            var estados = await _estadoRepository.GetCidadesByUFAsync(UF);
            return Ok(estados);
        }
    }
}
