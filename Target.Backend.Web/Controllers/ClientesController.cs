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
using System.ComponentModel.DataAnnotations;
using Target.Backend.Web.Interfaces.Services;

namespace Target.Backend.Web.Controllers
{
    [ApiController]
    [Route("api/v1/clientes")]
    [Produces("application/json")]
    [ApiKey]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IClienteEnderecoRepository _clienteEnderecoRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public ClientesController(IClienteRepository clienteRepository, IClienteEnderecoRepository clienteEnderecoRepository, IUnitOfWork uow, IMapper mapper, ILoggerManager logger)
        {
            _clienteRepository = clienteRepository;
            _clienteEnderecoRepository = clienteEnderecoRepository;
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }
        /// <summary>
        /// GET: api/v1/clientes?sortOrder={inicio/fim}
        /// </summary>
        /// <returns>Lista de clientes</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes([FromQuery] string sortOrder)
        {
            _logger.LogInfo("Buscando todos os clientes do banco de dados");
            IEnumerable<Cliente> clientes = await _clienteRepository.GetClientes(sortOrder);
            _logger.LogInfo("Busca finalizada");
            return Ok(clientes);

        }
        /// <summary>
        /// GET: api/v1/clientes/rendamensal/{rendaMensal}
        /// </summary>
        /// <param name="rendaMensal">renda mensal do cliente</param>
        /// <returns>Lista de clientes com renda mensal superior a informada no parametro</returns>
        [HttpGet("rendamensal/{rendaMensal}")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientesByRenda(decimal rendaMensal)
        {
            _logger.LogInfo($"Buscando todos os clientes com renda mensal superior a {rendaMensal} no banco de dados");
            IEnumerable <Cliente> clientes = await _clienteRepository.GetClientesByRenda(rendaMensal);
            _logger.LogInfo("Busca finalizada");
            return Ok(clientes);
        }

        /// <summary>
        /// GET: api/v1/clientes/{id}
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns>um cliente pelo id informado</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            _logger.LogInfo($"Buscando cliente com id {id} no banco de dados");
            var cliente = await _clienteRepository.GetClienteByID(id);
            _logger.LogInfo("Busca finalizada");

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        /// <summary>
        /// GET: api/v1/clientes/{id}/endereco
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns>endereço do cliente pelo id informado</returns>
        [HttpGet("{id}/endereco")]
        public async Task<ActionResult<ClienteEnderecoDTO>> GetEndereco(int id)
        {
            _logger.LogInfo($"Buscando endereço do cliente com id {id} no banco de dados");
            var endereco = await _clienteEnderecoRepository.GetClienteEnderecoById(id);
            _logger.LogInfo("Busca finalizada");

            if (endereco == null)
            {
                return NotFound();
            }

            ClienteEnderecoDTO enderecoDTO = _mapper.Map<ClienteEnderecoDTO>(endereco);

            return enderecoDTO;
        }

        /// <summary>
        /// PUT: api/v1/clientes/{id}/endereco
        /// </summary>
        /// <param name="id">id do cliente a ser atualizado</param>
        /// <param name="enderecoAtualizadoDTO">objeto com os campos a serem atualizados</param>
        /// <returns>codigo http 204</returns>
        [HttpPut("{id}/endereco")]
        public async Task<ActionResult<ClienteEndereco>> PutEndereco(int id, ClienteEnderecoDTO enderecoAtualizadoDTO)
        {
            _logger.LogInfo($"Atualizando cliente com id {id} no banco de dados");

            ClienteEndereco endereco = await _clienteEnderecoRepository.GetClienteEnderecoById(id);
            if (endereco == null)
            {
                _logger.LogInfo($"Cliente com id {id} não encontrado");
                return NotFound("Cliente não encontrado");
            }

            if (enderecoAtualizadoDTO.Logradouro != null) endereco.Logradouro = enderecoAtualizadoDTO.Logradouro;
            if (enderecoAtualizadoDTO.Bairro != null) endereco.Bairro = enderecoAtualizadoDTO.Bairro;
            if (enderecoAtualizadoDTO.Cidade != null) endereco.Cidade = enderecoAtualizadoDTO.Cidade;
            if (enderecoAtualizadoDTO.Complemento != null) endereco.Complemento = enderecoAtualizadoDTO.Complemento;
            if (enderecoAtualizadoDTO.UF != null) endereco.UF = enderecoAtualizadoDTO.UF;
            if (enderecoAtualizadoDTO.CEP != null) endereco.CEP = enderecoAtualizadoDTO.CEP;

            var validator = new DataAnnotationsValidator.DataAnnotationsValidator();
            var validationResults = new List<ValidationResult>();

            if (!validator.TryValidateObject(endereco, validationResults))
            {
                return BadRequest(validationResults);
            }

            try
            {
                await _uow.Commit();
                _logger.LogInfo($"Atualização do cliente finalizada");
            }
            catch (DbUpdateConcurrencyException)
            {
                _logger.LogInfo($"Erro ao atualizar endereço do cliente");
                return BadRequest("Erro ao atualizar endereço no banco de dados");
            }
            return NoContent();
        }

        /// <summary>
        /// POST: api/v1/clientes
        /// </summary>
        /// <param name="clienteDTO">objeto com dados do cliente a ser inserido</param>
        /// <returns>objeto com a confirmação do cadastro e com oferecimento do plano vip</returns>
        [HttpPost]
        public async Task<ActionResult> PostCliente(ClienteDTO clienteDTO)
        {

            Cliente cliente = _mapper.Map<Cliente>(clienteDTO);

            ClienteEndereco clienteEndereco = _mapper.Map<ClienteEndereco>(clienteDTO.Endereco);
            clienteEndereco.Cliente = cliente;

            var validator = new DataAnnotationsValidator.DataAnnotationsValidator();
            var validationResults = new List<ValidationResult>();
            bool clienteIsValid = validator.TryValidateObject(cliente, validationResults);
            bool enderecoIsValid = validator.TryValidateObject(clienteEndereco, validationResults);

            if (!(clienteIsValid && enderecoIsValid))
            {
                return BadRequest(validationResults);
            }

            _logger.LogInfo($"Inserindo Cliente no banco de dados");
            _clienteRepository.InsertCliente(cliente);
            _logger.LogInfo($"Inserindo Endereço do Cliente no banco de dados");
            _clienteEnderecoRepository.InsertClienteEndereco(clienteEndereco);

            int commit = await _uow.Commit();
            _logger.LogInfo($"Cliente e endereço inseridos com sucesso");
            bool transactionConfirmation = Convert.ToBoolean(commit);

            bool planoVip = cliente.RendaMensal >= 6000 ? true : false;
            var retorno =  new { Cadastro = transactionConfirmation, OferecerPlanoVip = planoVip };
            return Created("PostCliente", retorno);
        }

        /// <summary>
        /// POST: api/v1/clientes/{id}/confirmarplanovip
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns>confirmação de atualização para o plano vip</returns>
        [HttpPost("{id}/confirmarplanovip")]
        public async Task<ActionResult<bool>> PostClienteVip(int id)
        {
            _logger.LogInfo($"Atualizando plano de cliente com id {id}");
            Cliente cliente = await _clienteRepository.GetClienteByID(id);
            if (cliente == null) return NotFound("Cliente não encontrado");
            if (cliente.RendaMensal <= 6000) return BadRequest("Cliente não possui renda superior a 6000 reais");

            _clienteRepository.UpdatePlanoCliente(cliente);
            int commit = await _uow.Commit();
            _logger.LogInfo("Plano atualizado com sucesso");
            bool transactionConfirmation = Convert.ToBoolean(commit);
            return Ok(transactionConfirmation);
        }
    }
}
