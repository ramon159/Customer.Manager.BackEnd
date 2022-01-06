using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Target.Backend.Web.DTO;
using Target.Backend.Web.Interfaces.Repositories;
using Target.Backend.Web.Models;

namespace Target.Backend.Web.Controllers
{
    [Route("api/v1/planovip")]
    [ApiController]
    [Produces("application/json")]
    //[ApiKey]
    public class PlanoVipController : ControllerBase
    {
        private IMapper _mapper;
        private IPlanoRepository _planoRepository;
        public PlanoVipController(IMapper mapper, IPlanoRepository planoRepository)
        {
            _mapper = mapper;
            _planoRepository = planoRepository;
        }
        /// <summary>
        /// GET: api/v1/planovip
        /// </summary>
        /// <returns>detalhes do plano vip</returns>
        [HttpGet]
        public async Task<ActionResult<PlanoDTO>> GetPlanoVip()
        {
            Plano planoVip = await _planoRepository.GetPlanoByID(1);
            PlanoDTO planoDTO = _mapper.Map<PlanoDTO>(planoVip);
            return Ok(planoDTO);
        }
        /// <summary>
        /// GET: api/v1/planovip/indice
        /// </summary>
        /// <returns></returns>
        [HttpGet("indice")]
        public async Task<ActionResult<PlanoDTO>> GetIndiceAdesaoGeralPlanoVip()
        {
            int indice = await _planoRepository.GetIndiceAdesaoPlano();
            return Ok(indice);
        }

    }
}
