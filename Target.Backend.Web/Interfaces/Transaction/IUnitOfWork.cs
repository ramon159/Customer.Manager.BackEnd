using System.Threading.Tasks;

namespace Target.Backend.Web.Interfaces.Transaction
{
    public interface IUnitOfWork
    {
        Task<int> Commit();
        void Rollback();
    }
}
