using System.Collections.Generic;
using System.Threading.Tasks;
using Target.Backend.Web.Models;

namespace Target.Backend.Web.Interfaces.Repositories
{
    public interface IClienteRepository
    {
        /// <summary>
        ///  Obtem uma lista de clientes
        /// </summary>  
        /// <returns>Uma lista de clientes</returns>
        public Task<IEnumerable<Cliente>> GetClientes();
        /// <summary>
        ///  Obtem um cliente com base no id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns>um objeto Cliente</returns>
        public Task<Cliente> GetClienteByID(int id);
        /// <summary>
        /// Insere um cliente
        /// </summary>
        /// <param name="cliente"></param>
        public void InsertCliente(Cliente cliente);
    }
}
