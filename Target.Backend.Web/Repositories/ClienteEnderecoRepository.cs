﻿using System.Threading.Tasks;
using Target.Backend.Web.Interfaces.Repositories;
using Target.Backend.Web.Models;
using Target.Backend.Web.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace Target.Backend.Web.Repositories
{
    public class ClienteEnderecoRepository : IClienteEnderecoRepository
    {
        private readonly ApplicationContext _context;

        public ClienteEnderecoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ClienteEndereco> GetClienteEnderecoById(int id)
        {
            return await _context.ClienteEndereco.FirstOrDefaultAsync(e => e.ClienteId == id);
        }

        public void InsertClienteEndereco(ClienteEndereco ClienteEndereco)
        {
            _context.ClienteEndereco.Add(ClienteEndereco);
        }
    }
}
