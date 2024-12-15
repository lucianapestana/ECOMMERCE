using ECOMMERCE.API.DATA.Context;
using ECOMMERCE.API.DATA.Models.Tables;
using ECOMMERCE.API.Models.DTO;
using ECOMMERCE.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECOMMERCE.API.Repository
{
    public class ItemPedidoRepository(EcommerceContext _context) : IItemPedidoRepository
    {
        private readonly EcommerceContext _context = _context;

        public async Task<bool> AdicionarItemPedido(ItemPedidoDTO dto)
        {
            try
            {
                var itemPedido = new TB_ITENS_PEDIDO
                {
                    PEDIDO_ID = dto.PedidoId,
                    PRODUTO_ID = dto.ProdutoId,
                    QUANTIDADE = dto.Quantidade
                };

                _context.TB_ITENS_PEDIDO.Add(itemPedido);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar o registro", ex);
            }
        }

        public async Task<bool> AtualizarItemPedido(ItemPedidoDTO dto)
        {
            try
            {
                var itemPedido = new TB_ITENS_PEDIDO
                {
                    PEDIDO_ID = dto.PedidoId,
                    PRODUTO_ID = dto.ProdutoId,
                    QUANTIDADE = dto.Quantidade
                };

                _context.TB_ITENS_PEDIDO.Add(itemPedido);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar o registro", ex);
            }
        }

        public async Task<bool> RemoverItemPedido(int itemPedidoId)
        {
            using (var transaction = _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var itemPedido = await _context.Set<TB_ITENS_PEDIDO>()
                        .Where(item => item.ITEM_PEDIDO_ID == itemPedidoId)
                        .FirstOrDefaultAsync();

                    if (itemPedido == null)
                    {
                        await transaction.Result.RollbackAsync();
                        return false;
                    }

                    _context.Set<TB_ITENS_PEDIDO>().Remove(itemPedido);
                    await _context.SaveChangesAsync();
                    await transaction.Result.CommitAsync();

                    return true;
                }
                catch (Exception)
                {
                    await transaction.Result.RollbackAsync();
                    return false;
                }
            }
        }
    }
}
