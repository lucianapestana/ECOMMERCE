using ECOMMERCE.API.Models.DTO;

namespace ECOMMERCE.API.Repository.Interfaces
{
    public interface IPedidoRepository
    {
        Task<FaturamentoDTO> AdicionarPedidoItens(PedidoItemDTO pedidoItem);
    }
}
