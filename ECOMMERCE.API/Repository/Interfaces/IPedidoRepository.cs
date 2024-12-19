using ECOMMERCE.API.Models;
using ECOMMERCE.API.Models.DTO;

namespace ECOMMERCE.API.Repository.Interfaces
{
    public interface IPedidoRepository
    {
        Task<FaturamentoDTO> AdicionarPedidoItens(PedidoItemDTO pedidoItem);

        Task<bool> AtualizarPedido(PedidoDTO dto);

        Task<List<Pedido>> ObterPedidos(string? pedidoId = null);
    }
}
