using ECOMMERCE.API.Models;
using ECOMMERCE.API.Models.DTO;

namespace ECOMMERCE.API.Repository.Interfaces
{
    public interface IItemPedidoRepository
    {
        Task<bool> AdicionarItemPedido(ItemPedidoDTO dto);

        Task<bool> AtualizarItemPedido(ItemPedidoDTO dto);

        Task<bool> RemoverItemPedido(string pedidoId);

        Task<List<ItensPedido>> ObterTodos(string? pedidoId = null);
    }
}
