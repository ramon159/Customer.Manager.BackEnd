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

namespace Target.Backend.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    //[ApiKey]
    public class ClientesController : ControllerBase
    {
        private IClienteRepository _clienteRepository;
        private IClienteEnderecoRepository _clienteEnderecoRepository;
        private IUnitOfWork _uow;

        public ClientesController(IClienteRepository clienteRepository, IClienteEnderecoRepository clienteEnderecoRepository, IUnitOfWork uow)
        {
            _clienteRepository = clienteRepository;
            _clienteEnderecoRepository = clienteEnderecoRepository;
            _uow = uow;
        }

        // GET: api/v1/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return Ok(await _clienteRepository.GetClientes());
        }

        // GET: api/v1/Clientes/5
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

        // POST: api/v1/Clientes
        [HttpPost]
        public async Task<ActionResult> PostCliente(ClienteDTO clienteDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Cliente cliente = new Cliente()
            {
                NomeCompleto = clienteDTO.NomeCompleto,
                CPF = clienteDTO.CPF,
                DataNascimento = clienteDTO.DataNascimento,
                RendaMensal = clienteDTO.RendaMensal,
            };
            ClienteEndereco clienteEndereco = new ClienteEndereco() 
            {
                Logradouro = clienteDTO.Endereco.Logradouro,
                Bairro = clienteDTO.Endereco.Bairro,
                Complemento = clienteDTO.Endereco.Complemento,
                CEP = clienteDTO.Endereco.CEP,
                Cidade = clienteDTO.Endereco.Cidade,
                UF = clienteDTO.Endereco.UF,
                Cliente = cliente
            };

            _clienteRepository.InsertCliente(cliente);
            _clienteEnderecoRepository.InsertClienteEndereco(clienteEndereco);

            int transactionConfirmation = await _uow.Commit();

            bool planoVip = cliente.RendaMensal >= 6000 ? true : false;
            var retorno =  new { Cadastro = Convert.ToBoolean(transactionConfirmation), OferecerPlanoVip = planoVip };
            return Created("PostCliente", retorno);
        }
    }
}
