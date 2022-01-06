using System.Threading.Tasks;
using Target.Backend.Web.Models;

namespace Target.Backend.Web.Interfaces.Repositories
{
    public interface IClienteEnderecoRepository
    {
        /// <summary>
        /// Insere o endereço do cliente
        /// </summary>
        /// <param name="ClienteEndereco">endereço do cliente</param>
        public void InsertClienteEndereco(ClienteEndereco ClienteEndereco);
        /// <summary>
        /// Obtem o endereço pelo id do cliente
        /// </summary>
        /// <param name="id">id do cliente</param>
        public Task<ClienteEndereco> GetClienteEnderecoById(int id);
    }
}
