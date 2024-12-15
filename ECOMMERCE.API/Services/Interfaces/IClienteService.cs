using ECOMMERCE.API.Models.Api;
using ECOMMERCE.API.Models.DTO;

namespace ECOMMERCE.API.Services.Interfaces
{
    public interface IClienteService
    {
        Task<bool> AdicionarCliente(ClienteDTO dto);

        Task<bool> AtualizarCliente(ClienteDTO dto);

        List<Error> ValidarCliente(ClienteDTO dto, bool adicionar = false);
    }
}
