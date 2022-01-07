using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Target.Backend.Web.DTO;
using Target.Backend.Web.Interfaces.Repositories;
using Target.Backend.Web.Models;
using Target.Backend.Web.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Target.Backend.Web.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationContext _context;
        private IPlanoRepository _planoRepository;
        public ClienteRepository(ApplicationContext context, IPlanoRepository planoRepository)
        {
            _context = context;
            _planoRepository = planoRepository;
        }

        public async Task<Cliente> GetClienteByID(int id)
        {
            try
            {
                return await _context.Cliente
                    .Include(c => c.Endereco)
                    .Include(c => c.Plano)
                    .FirstOrDefaultAsync(c => c.Id == id);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Cliente>> GetClientes(string sortOrder)
        {
            try
            {
                var result = sortOrder == "fim" ?
                     await _context.Cliente
                    .Include(c => c.Endereco)
                    .Include(c => c.Plano)
                    .OrderByDescending(c => c.CriadoEm)
                    .ToListAsync() : 
                    await _context.Cliente
                    .Include(c => c.Endereco)
                    .Include(c => c.Plano)
                    .OrderBy(c => c.CriadoEm)
                    .ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Cliente>> GetClientesByRenda(decimal renda)
        {
            try
            {
                return await _context.Cliente
                .Where(c => c.RendaMensal >= renda)
                .Include(c => c.Endereco)
                .Include(c => c.Plano)
                .OrderByDescending(c => c.RendaMensal)
                .ToListAsync();
            } 
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void InsertCliente(Cliente cliente)
        {
            _context.Cliente.Add(cliente);
        }

        public async void UpdatePlanoCliente(Cliente cliente)
        {
            try
            {
                Plano plano = await _planoRepository.GetPlanoByID(1);
                cliente.Plano = plano;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}
