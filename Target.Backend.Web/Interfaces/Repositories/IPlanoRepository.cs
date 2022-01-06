using System.Threading.Tasks;
using Target.Backend.Web.Models;

namespace Target.Backend.Web.Interfaces.Repositories
{
    public interface IPlanoRepository
    {
        /// <summary>
        /// Obtem plano pelo id
        /// </summary>
        /// <param name="id">id do plano</param>
        /// <returns>objeto do tipo Plano</returns>
        public Task<Plano> GetPlanoByID(int id);
        /// <summary>
        /// Obtem um indice geral de adesão do plano
        /// </summary>
        /// <returns>indice com o total de clientes que podem assinar o plano vip</returns>
        public Task<int> GetIndiceAdesaoPlano();
    }
}
