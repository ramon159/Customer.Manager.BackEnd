
using System.Threading.Tasks;
using Target.Backend.Web.Data;
using Target.Backend.Web.Interfaces.Transaction;

namespace Target.Backend.Web.Transaction
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<int> Commit()
        {
            return await _context.SaveChangesAsync();
        }

        public void Rollback()
        {
            // não implementado
        }
    }
}
