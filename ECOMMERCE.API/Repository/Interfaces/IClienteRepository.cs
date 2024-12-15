using ECOMMERCE.API.Models;
using ECOMMERCE.API.Models.DTO;

namespace ECOMMERCE.API.Repository.Interfaces
{
    public interface IClienteRepository
    {
        Task<bool> AdicionarCliente(ClienteDTO dto);

        Task<bool> AtualizarCliente(ClienteDTO dto);

        Task<IEnumerable<Cliente>> ObterTodos(string? clienteId = null);
    }
}
