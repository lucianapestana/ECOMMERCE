using ECOMMERCE.API.Models.DTO;

namespace ECOMMERCE.API.Services.Interfaces
{
    public interface IPedidoService
    {
        Task<bool> AdicionarPedidoItens(PedidoItemDTO dto);
    }
}
