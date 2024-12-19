using ECOMMERCE.API.DATA.Context;
using ECOMMERCE.API.DATA.Models.Tables;
using ECOMMERCE.API.Models;
using ECOMMERCE.API.Models.DTO;
using ECOMMERCE.API.Models.Enums;
using ECOMMERCE.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECOMMERCE.API.Repository
{
    public class PedidoRepository(EcommerceContext _context) : IPedidoRepository
    {
        private readonly EcommerceContext _context = _context;

        public async Task<FaturamentoDTO> AdicionarPedidoItens(PedidoItemDTO pedidoItem)
        {
            using (var transaction = _context.Database.BeginTransactionAsync())
            {
                var faturamento = new FaturamentoDTO();

                try
                {
                    var pedido = new TB_PEDIDOS
                    {
                        PEDIDO_ID = Guid.NewGuid().ToString().ToUpper(),
                        USUARIO_INSERCAO_ID = 1,
                        DATA_INSERCAO = DateTime.Now,
                        DATA_VENDA = pedidoItem.DataVenda != null ? pedidoItem.DataVenda.Value : DateTime.Now,
                        CLIENTE_ID = pedidoItem.Cliente?.ClienteId ?? string.Empty,
                        STATUS_PEDIDO_ID = StatusPedidoEnum.ABERTO.GetHashCode()
                    };

                    _context.TB_PEDIDOS.Add(pedido);
                    await _context.SaveChangesAsync();

                    faturamento.PedidoId = pedido.PEDIDO_ID;

                    foreach (var item in pedidoItem.Itens)
                    {
                        var valorItem = (item.Quantidade * item.PrecoUnitario);
                        faturamento.SubTotal += valorItem;

                        var itemPedido = new TB_ITENS_PEDIDO
                        {
                            PEDIDO_ID = pedido.PEDIDO_ID,
                            PRODUTO_ID = item.ProdutoId,
                            DESCRICAO = item.Descricao ?? string.Empty,
                            QUANTIDADE = item.Quantidade,
                            PRECO_UNITARIO = item.PrecoUnitario
                        };
                        _context.TB_ITENS_PEDIDO.Add(itemPedido);
                        await _context.SaveChangesAsync();
                    }

                    await transaction.Result.CommitAsync();
                    return faturamento;
                }
                catch (Exception)
                {
                    await transaction.Result.RollbackAsync();
                    return faturamento;
                }
            }
        }

        public async Task<bool> AtualizarPedido(PedidoDTO dto)
        {
            try
            {
                var pedidoExistente = _context.TB_PEDIDOS.Find(dto.PedidoId);

                if (pedidoExistente != null)
                {
                    pedidoExistente.USUARIO_ATUALIZACAO_ID = 1;
                    pedidoExistente.DATA_ATUALIZACAO = DateTime.Now;
                    pedidoExistente.DATA_VENDA = dto.DataVenda;
                    pedidoExistente.CLIENTE_ID = dto.ClienteId ?? string.Empty;
                    pedidoExistente.STATUS_PEDIDO_ID = dto.StatusPedidoId;

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

        public async Task<List<Pedido>> ObterPedidos(string? pedidoId = null)
        {
            try
            {
                var result = await (
                    from pedido in _context.TB_PEDIDOS
                    join status_pedido in _context.TB_STATUS_PEDIDOS on pedido.STATUS_PEDIDO_ID equals status_pedido.STATUS_PEDIDO_ID
                    join cliente in _context.TB_CLIENTES on pedido.CLIENTE_ID equals cliente.CLIENTE_ID
                    join c in _context.TB_CATEGORIAS on cliente.CATEGORIA_ID equals c.CATEGORIA_ID into c_categoria
                    from categoria in c_categoria.DefaultIfEmpty()
                    join faturamento in _context.TB_FATURAMENTOS on pedido.PEDIDO_ID equals faturamento.PEDIDO_ID
                    where
                    (
                        (
                            !string.IsNullOrWhiteSpace(pedidoId)
                            && pedido.PEDIDO_ID == pedidoId
                        ) || string.IsNullOrWhiteSpace(pedidoId)
                    )
                    select new Pedido()
                    {
                        PedidoId = pedido.PEDIDO_ID,
                        UsuarioInsercaoId = pedido.USUARIO_INSERCAO_ID,
                        UsuarioAtualizacaoId = pedido.USUARIO_ATUALIZACAO_ID,
                        DataVenda = pedido.DATA_VENDA,
                        ClienteId = pedido.CLIENTE_ID,
                        Cliente = cliente != null ? new Cliente()
                        {
                            ClienteId = cliente.CLIENTE_ID,
                            Ativo = cliente.ATIVO,
                            UsuarioInsercaoId = cliente.USUARIO_INSERCAO_ID,
                            DataInsercao = cliente.DATA_INSERCAO,
                            UsuarioAtualizacaoId = cliente.USUARIO_ATUALIZACAO_ID,
                            DataAtualizacao = cliente.DATA_ATUALIZACAO,
                            Nome = cliente.NOME,
                            Cpf = cliente.CPF,
                            CategoriaId = cliente.CATEGORIA_ID,
                            Categoria = categoria != null ? new Categoria()
                            {
                                CategoriaId = categoria.CATEGORIA_ID,
                                Ativo = categoria.ATIVO,
                                UsuarioInsercaoId = categoria.USUARIO_INSERCAO_ID,
                                DataInsercao = categoria.DATA_INSERCAO,
                                UsuarioAtualizacaoId = categoria.USUARIO_ATUALIZACAO_ID,
                                DataAtualizacao = categoria.DATA_ATUALIZACAO,
                                Descricao = categoria.DESCRICAO,
                                PorcentagemDesconto = categoria.PORCENTAGEM_DESCONTO
                            } : null,
                        } : null,
                        StatusPedidoId = pedido.STATUS_PEDIDO_ID,
                        StatusPedido = status_pedido != null ? new StatusPedido()
                        {
                            StatusPedidoId = status_pedido.STATUS_PEDIDO_ID,
                            Ativo = status_pedido.ATIVO,
                            UsuarioInsercaoId = status_pedido.USUARIO_INSERCAO_ID,
                            DataInsercao = status_pedido.DATA_INSERCAO,
                            UsuarioAtualizacaoId = status_pedido.USUARIO_ATUALIZACAO_ID,
                            DataAtualizacao = status_pedido.DATA_ATUALIZACAO,
                            Descricao = status_pedido.DESCRICAO
                        } : null,
                        Faturamento = faturamento != null ? new Faturamento()
                        {
                            FaturamentoId = faturamento.FATURAMENTO_ID,
                            PedidoId = faturamento.PEDIDO_ID,
                            SubTotal = faturamento.SUB_TOTAL,
                            Descontos = faturamento.DESCONTOS,
                            ValorTotal = faturamento.VALOR_TOTAL
                        } : null,
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
