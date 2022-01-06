using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Target.Backend.Web.Attributes;
using Target.Backend.Web.Interfaces.Repositories;
using Target.Backend.Web.Models;
using Target.Backend.Web.Data;
using Target.Backend.Web.DTO;
using Target.Backend.Web.Interfaces.Transaction;
using AutoMapper;

namespace Target.Backend.Web.Controllers
{
    [ApiController]
    [Route("api/v1/clientes")]
    [Produces("application/json")]
    //[ApiKey]
    public class ClientesController : ControllerBase
    {
        private IClienteRepository _clienteRepository;
        private IClienteEnderecoRepository _clienteEnderecoRepository;
        private IUnitOfWork _uow;
        private IMapper _mapper;

        public ClientesController(IClienteRepository clienteRepository, IClienteEnderecoRepository clienteEnderecoRepository, IUnitOfWork uow, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _clienteEnderecoRepository = clienteEnderecoRepository;
            _uow = uow;
            _mapper = mapper;
        }

        // GET: api/v1/clientes?sortOrder=fim
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes([FromQuery] string sortOrder)
        {
            return Ok(await _clienteRepository.GetClientes(sortOrder));
        }
        // GET: api/v1/clientes/rendamensal/{rendaMensal}
        [HttpGet("rendamensal/{rendaMensal}")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientesByRenda(decimal rendaMensal)
        {
            return Ok(await _clienteRepository.GetClientesByRenda(rendaMensal));
        }

        // GET: api/v1/clientes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _clienteRepository.GetClienteByID(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }       
        // GET: api/v1/clientes/{id}/endereco
        [HttpGet("{id}/endereco")]
        public async Task<ActionResult<ClienteEnderecoDTO>> GetEndereco(int id)
        {
            var cliente = await _clienteRepository.GetClienteByID(id);

            if (cliente == null)
            {
                return NotFound();
            }
            ClienteEnderecoDTO endereco = _mapper.Map<ClienteEnderecoDTO>(cliente.Endereco);

            return endereco;
        }

        // POST: api/v1/clientes
        [HttpPost]
        public async Task<ActionResult> PostCliente(ClienteDTO clienteDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Cliente cliente = _mapper.Map<Cliente>(clienteDTO);


            ClienteEndereco clienteEndereco = _mapper.Map<ClienteEndereco>(clienteDTO.Endereco);
            clienteEndereco.Cliente = cliente;

            _clienteRepository.InsertCliente(cliente);
            _clienteEnderecoRepository.InsertClienteEndereco(clienteEndereco);

            int commit = await _uow.Commit();
            bool transactionConfirmation = Convert.ToBoolean(commit);

            bool planoVip = cliente.RendaMensal >= 6000 ? true : false;
            var retorno =  new { Cadastro = transactionConfirmation, OferecerPlanoVip = planoVip };
            return Created("PostCliente", retorno);
        }

        // POST: api/v1/clientes/{id}/confirmarplanovip
        [HttpPost("{id}/confirmarplanovip")]
        public async Task<ActionResult<bool>> PostClienteVip(int id)
        {
            Cliente cliente = await _clienteRepository.GetClienteByID(id);
            if (cliente == null) return NotFound("Cliente não encontrado");
            if (cliente.RendaMensal <= 6000) return BadRequest("Cliente não possui renda superior a 6000 reais");

            _clienteRepository.UpdatePlanoCliente(cliente);
            int commit = await _uow.Commit();
            bool transactionConfirmation = Convert.ToBoolean(commit);
            return Ok(transactionConfirmation);
        }
    }
}
