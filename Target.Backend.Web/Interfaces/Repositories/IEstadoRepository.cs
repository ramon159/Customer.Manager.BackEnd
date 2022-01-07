using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Target.Backend.Web.Interfaces.Repositories
{
    public interface IEstadoRepository
    {
        /// <summary>
        /// Método que obtem estados brasileiros
        /// </summary>
        /// <returns>objeto com estados brasileiro</returns>
        public Task<object> GetEstadosAsync();
        /// <summary>
        /// Método que obtem cidades pela UF informada
        /// </summary>
        /// <param name="UF">sigla da unidade federal</param>
        /// <returns>objeto com as cidades da UF informada</returns>
        public Task<object> GetCidadesByUFAsync(string UF);
    }
}
