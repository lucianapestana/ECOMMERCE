using ECOMMERCE.API.Models.DTO;
using ECOMMERCE.API.Models.Enums;
using ECOMMERCE.API.Models.Outputs;
using ECOMMERCE.API.Repository.Interfaces;
using ECOMMERCE.API.Services.Interfaces;

namespace ECOMMERCE.API.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IFaturamentoRepository _faturamentoRepository;
        private readonly IItemPedidoRepository _itemPedidoRepository;

        public PedidoService
            (
                IPedidoRepository pedidoRepository,
                ICategoriaRepository categoriaRepository,
                IFaturamentoRepository faturamentoRepository,
                IItemPedidoRepository itemPedidoRepository
            )
        {
            _pedidoRepository = pedidoRepository;
            _categoriaRepository = categoriaRepository;
            _faturamentoRepository = faturamentoRepository;
            _itemPedidoRepository = itemPedidoRepository;
        }

        public async Task<bool> AdicionarPedidoItens(PedidoItemDTO dto)
        {
            try
            {
                var faturamento = await _pedidoRepository.AdicionarPedidoItens(dto);

                if (!string.IsNullOrWhiteSpace(faturamento.PedidoId) && faturamento.PedidoId != "0")
                {
                    var categoriaCliente = await _categoriaRepository.ObterTodas(dto.Cliente.Categoria);
                    var porcentagemDesconto = categoriaCliente?.First().PorcentagemDesconto;

                    if ((dto.Cliente.Categoria == ClienteCategoriaEnum.REGULAR.GetHashCode() && faturamento.SubTotal > 500) ||
                        (dto.Cliente.Categoria == ClienteCategoriaEnum.PREMIUM.GetHashCode() && faturamento.SubTotal > 300) ||
                        (dto.Cliente.Categoria == ClienteCategoriaEnum.VIP.GetHashCode()))
                    {
                        var descontos = faturamento.Descontos.HasValue ? faturamento.Descontos.Value : 0;
                        faturamento.Descontos = (faturamento.SubTotal * (porcentagemDesconto / 100));
                        faturamento.ValorTotal = (faturamento.SubTotal - descontos);
                    }
                    else
                    {
                        faturamento.Descontos = 0;
                        faturamento.ValorTotal = faturamento.SubTotal;
                    }

                    return await _faturamentoRepository.AdicionarFaturamentoVenda(faturamento);
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar o registro", ex);
            }
        }

        public async Task<PedidoVendaOutput> AtualizarPedidoItens(PedidoItemDTO dto)
        {
            try
            {
                var output = new PedidoVendaOutput();

                var result = await this.ObterPedidos(dto.Identificador);
                var pedidos = result.Pedidos;

                if (pedidos != null && pedidos.First().StatusPedidoId == StatusPedidoEnum.ABERTO.GetHashCode())
                {
                    var pedidoDTO = new PedidoDTO()
                    {
                        PedidoId = dto.Identificador,
                        DataVenda = dto.DataVenda != null ? dto.DataVenda.Value : pedidos.First().DataVenda,
                        ClienteId = dto.Cliente != null ? dto.Cliente.ClienteId : pedidos.First().ClienteId,
                        StatusPedidoId = dto.StatusPedido != null ? dto.StatusPedido.Value : pedidos.First().StatusPedidoId
                    };

                    var pedidoAtualizado = await _pedidoRepository.AtualizarPedido(pedidoDTO);

                    if (!pedidoAtualizado)
                    {
                        output.Codigo = "400";
                        output.MensagemCodigo = "Ocorreu um erro ao alterar o pedido.";
                        return output;
                    }

                    if (dto.Itens != null && dto.Itens.Count > 0)
                    {
                        var pedidoId = pedidos.First().PedidoId;

                        var pedidosRemovidos = await _itemPedidoRepository.RemoverItemPedido(pedidoId);

                        if (!pedidosRemovidos)
                        {
                            output.Codigo = "400";
                            output.MensagemCodigo = "Ocorreu um erro ao alterar o pedido.";
                            return output;
                        }

                        var faturamento = new FaturamentoDTO();
                        faturamento.FaturamentoId = pedidos.First().Faturamento?.FaturamentoId;
                        faturamento.PedidoId = pedidoId;

                        foreach (var item in dto.Itens)
                        {
                            var valorItem = (item.Quantidade * item.PrecoUnitario);
                            faturamento.SubTotal += valorItem;
                            item.PedidoId = pedidoId;

                            await _itemPedidoRepository.AdicionarItemPedido(item);
                        }

                        if (pedidos.First().Faturamento != null)
                        {
                            var categoriaCliente = pedidos.First().Cliente?.Categoria?.CategoriaId;
                            var porcentagemDesconto = pedidos.First().Cliente?.Categoria?.PorcentagemDesconto;

                            if ((categoriaCliente == ClienteCategoriaEnum.REGULAR.GetHashCode() && faturamento.SubTotal > 500) ||
                                (categoriaCliente == ClienteCategoriaEnum.PREMIUM.GetHashCode() && faturamento.SubTotal > 300) ||
                                (categoriaCliente == ClienteCategoriaEnum.VIP.GetHashCode()))
                            {
                                var descontos = faturamento.Descontos.HasValue ? faturamento.Descontos.Value : 0;
                                faturamento.Descontos = (faturamento.SubTotal * (porcentagemDesconto / 100));
                                faturamento.ValorTotal = (faturamento.SubTotal - descontos);
                            }
                            else
                            {
                                faturamento.Descontos = 0;
                                faturamento.ValorTotal = faturamento.SubTotal;
                            }

                            await _faturamentoRepository.AtualizarFaturamento(faturamento);

                        }
                    }

                    output.Codigo = "200";
                    output.MensagemCodigo = "Pedido alterado com sucesso.";
                }
                else
                {
                    output.Codigo = "400";
                    output.MensagemCodigo = "O pedido não pode ser alterado pois não está com status ABERTO.";
                }

                return output;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar o registro", ex);
            }
        }

        public async Task<PedidoVendaOutput> ObterPedidos(string? pedidoId = null)
        {
            try
            {
                var pedidoVenda = new PedidoVendaOutput();

                pedidoVenda.Pedidos = await _pedidoRepository.ObterPedidos(pedidoId);

                foreach (var pedido in pedidoVenda.Pedidos)
                {
                    var itensPedido = await _itemPedidoRepository.ObterTodos(pedido.PedidoId);
                    pedido.ItensPedido.AddRange(itensPedido);
                }

                return pedidoVenda;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar o registro", ex);
            }
        }
    }
}
