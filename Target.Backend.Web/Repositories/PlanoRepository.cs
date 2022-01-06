using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Target.Backend.Web.Data;
using Target.Backend.Web.Interfaces.Repositories;
using Target.Backend.Web.Models;

namespace Target.Backend.Web.Repositories
{
    public class PlanoRepository : IPlanoRepository
    {
        private readonly ApplicationContext _context;
        public PlanoRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Plano> GetPlanoByID(int id)
        {
            return await _context.Plano.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<int> GetIndiceAdesaoPlano()
        {
            int indice = await _context.Cliente.CountAsync(c => c.PlanoId == null && c.RendaMensal >= 6000);
            return indice;
        }
    }
}
