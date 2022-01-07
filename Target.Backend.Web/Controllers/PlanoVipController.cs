using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Target.Backend.Web.Attributes;
using Target.Backend.Web.DTO;
using Target.Backend.Web.Interfaces.Repositories;
using Target.Backend.Web.Interfaces.Services;
using Target.Backend.Web.Models;

namespace Target.Backend.Web.Controllers
{
    [Route("api/v1/planovip")]
    [ApiController]
    [Produces("application/json")]
    [ApiKey]
    public class PlanoVipController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPlanoRepository _planoRepository;
        private readonly ILoggerManager _logger;
        public PlanoVipController(IMapper mapper, IPlanoRepository planoRepository, ILoggerManager logger)
        {
            _mapper = mapper;
            _planoRepository = planoRepository;
            _logger = logger;
        }
        /// <summary>
        /// GET: api/v1/planovip
        /// </summary>
        /// <returns>detalhes do plano vip</returns>
        [HttpGet]
        public async Task<ActionResult<PlanoDTO>> GetPlanoVip()
        {
            _logger.LogInfo($"Buscando detalhes do plano vip no banco de dados");
            Plano planoVip = await _planoRepository.GetPlanoByID(1);
            _logger.LogInfo("Busca finalizada");
            PlanoDTO planoDTO = _mapper.Map<PlanoDTO>(planoVip);
            return Ok(planoDTO);
        }
        /// <summary>
        /// GET: api/v1/planovip/indice
        /// </summary>
        /// <returns>indice de adesão do plano vip</returns>
        [HttpGet("indice")]
        public async Task<ActionResult<PlanoDTO>> GetIndiceAdesaoGeralPlanoVip()
        {
            _logger.LogInfo($"Buscando indice com a quantidade de clientes sem o plano vip no banco de dados");
            int indice = await _planoRepository.GetIndiceAdesaoPlano();
            _logger.LogInfo("Busca finalizada");
            return Ok(indice);
        }

    }
}
