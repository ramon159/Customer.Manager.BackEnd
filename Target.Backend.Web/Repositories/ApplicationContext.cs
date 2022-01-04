using Microsoft.EntityFrameworkCore;
using System;
using Target.Backend.Web.Models;

namespace Target.Example.Web.Repositories
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) 
        {
        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ClienteEndereco> Enderecos { get; set; }
        public DbSet<Plano> Planos { get; set; }
    }
}
