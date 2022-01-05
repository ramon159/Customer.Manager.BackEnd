using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Target.Backend.Web.Models;

namespace Target.Backend.Web.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) 
        {
        }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<ClienteEndereco> ClienteEndereco { get; set; }
        public DbSet<Plano> Plano { get; set; }
        
    }
}
