using ECOMMERCE.API.DATA.Context;
using ECOMMERCE.API.DATA.Models.Tables;
using ECOMMERCE.API.Models;
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
                    PEDIDO_ID = dto.PedidoId ??string.Empty,
                    PRODUTO_ID = dto.ProdutoId,
                    DESCRICAO = dto.Descricao ?? string.Empty,
                    QUANTIDADE = dto.Quantidade,
                    PRECO_UNITARIO = dto.PrecoUnitario
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
                var itemExistente = _context.TB_ITENS_PEDIDO.Find(dto.ItemPedidoId);

                if (itemExistente != null)
                {
                    itemExistente.PEDIDO_ID = dto.PedidoId ?? string.Empty;
                    itemExistente.PRODUTO_ID = dto.ProdutoId;
                    itemExistente.DESCRICAO = dto.Descricao ?? string.Empty;
                    itemExistente.QUANTIDADE = dto.Quantidade;
                    itemExistente.PRECO_UNITARIO = dto.PrecoUnitario;

                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar o registro", ex);
            }
        }

        public async Task<bool> RemoverItemPedido(string pedidoId)
        {
            using (var transaction = _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var itensPedido = await _context.Set<TB_ITENS_PEDIDO>()
                        .Where(item => item.PEDIDO_ID == pedidoId)
                        .ToListAsync();

                    if (itensPedido != null && itensPedido.Count > 0)
                    {
                        foreach (var itemPedido in itensPedido)
                        {
                            _context.Set<TB_ITENS_PEDIDO>().Remove(itemPedido);
                            await _context.SaveChangesAsync();
                        }

                        await transaction.Result.CommitAsync();
                        return true;
                    }

                    await transaction.Result.RollbackAsync();
                    return false;
                }
                catch (Exception)
                {
                    await transaction.Result.RollbackAsync();
                    return false;
                }
            }
        }

        public async Task<List<ItensPedido>> ObterTodos(string? pedidoId = null)
        {
            try
            {
                var result = await (
                    from itensPedido in _context.TB_ITENS_PEDIDO
                    where
                    (
                        (
                            !string.IsNullOrWhiteSpace(pedidoId)
                            && itensPedido.PEDIDO_ID == pedidoId
                        ) || string.IsNullOrWhiteSpace(pedidoId)
                    )
                    select new ItensPedido()
                    {
                        ItemPedidoId = itensPedido.ITEM_PEDIDO_ID,
                        PedidoId = itensPedido.PEDIDO_ID,
                        ProdutoId = itensPedido.PRODUTO_ID,
                        Descricao = itensPedido.DESCRICAO,
                        Quantidade = itensPedido.QUANTIDADE,
                        PrecoUnitario = itensPedido.PRECO_UNITARIO
                    }
                    ).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao obter os registros", ex);
            }
        }
    }
}
