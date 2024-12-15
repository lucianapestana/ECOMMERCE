using ECOMMERCE.API.Models.DTO;

namespace ECOMMERCE.API.Repository.Interfaces
{
    public interface IPedidoRepository
    {
        Task<bool> AdicionarPedidoItens(PedidoItemDTO pedidoItem);
    }
}
