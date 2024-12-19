using ECOMMERCE.API.Models.DTO;
using ECOMMERCE.API.Models.Outputs;

namespace ECOMMERCE.API.Services.Interfaces
{
    public interface IPedidoService
    {
        Task<bool> AdicionarPedidoItens(PedidoItemDTO dto);

        Task<PedidoVendaOutput> AtualizarPedidoItens(PedidoItemDTO dto);

        Task<PedidoVendaOutput> ObterPedidos(string? pedidoId = null);
    }
}
