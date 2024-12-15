using ECOMMERCE.API.DATA.Context;
using ECOMMERCE.API.DATA.Models.Tables;
using ECOMMERCE.API.Models.DTO;
using ECOMMERCE.API.Models.Enums;
using ECOMMERCE.API.Repository.Interfaces;

namespace ECOMMERCE.API.Repository
{
    public class PedidoRepository(EcommerceContext _context) : IPedidoRepository
    {
        private readonly EcommerceContext _context = _context;

        public async Task<bool> AdicionarPedidoItens(PedidoItemDTO pedidoItem)
        {
            using (var transaction = _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var pedido = new TB_PEDIDOS
                    {
                        USUARIO_INSERCAO_ID = 1,
                        DATA_INSERCAO = DateTime.Now,
                        DATA_VENDA = pedidoItem.DataVenda,
                        CLIENTE_ID = pedidoItem.Cliente.ClienteId,
                        STATUS_PEDIDO_ID = StatusPedidoEnum.ABERTO.GetHashCode()
                    };

                    _context.TB_PEDIDOS.Add(pedido);
                    await _context.SaveChangesAsync();

                    int pedidoId = pedido.PEDIDO_ID;

                    foreach (var item in pedidoItem.Itens)
                    {
                        var itemPedido = new TB_ITENS_PEDIDO
                        {
                            PEDIDO_ID = pedidoId,
                            PRODUTO_ID = item.ProdutoId,
                            DESCRICAO = item.Descricao,
                            QUANTIDADE = item.Quantidade,
                            PRECO_UNITARIO = item.PrecoUnitario
                        };
                        _context.TB_ITENS_PEDIDO.Add(itemPedido);
                        await _context.SaveChangesAsync();
                    }
                    
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
