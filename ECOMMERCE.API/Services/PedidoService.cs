using ECOMMERCE.API.Models.DTO;
using ECOMMERCE.API.Models.Enums;
using ECOMMERCE.API.Repository.Interfaces;
using ECOMMERCE.API.Services.Interfaces;

namespace ECOMMERCE.API.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IFaturamentoRepository _faturamentoRepository;

        public PedidoService
            (
                IPedidoRepository pedidoRepository, 
                ICategoriaRepository categoriaRepository,
                IFaturamentoRepository faturamentoRepository
            )
        {
            _pedidoRepository = pedidoRepository;
            _categoriaRepository = categoriaRepository;
            _faturamentoRepository = faturamentoRepository;
        }

        public async Task<bool> AdicionarPedidoItens(PedidoItemDTO dto)
        {
            try
            {
                 var faturamento = await _pedidoRepository.AdicionarPedidoItens(dto);

                if (faturamento.PedidoId > 0)
                {
                    var categoriaCliente = await _categoriaRepository.ObterTodas(dto.Cliente.Categoria);
                    var porcentagemDesconto = categoriaCliente?.First().PorcentagemDesconto;
                    
                    if ((dto.Cliente.Categoria == ClienteCategoriaEnum.REGULAR.GetHashCode() && faturamento.SubTotal > 500) ||
                        (dto.Cliente.Categoria == ClienteCategoriaEnum.PREMIUM.GetHashCode() && faturamento.SubTotal > 300) ||
                        (dto.Cliente.Categoria == ClienteCategoriaEnum.VIP.GetHashCode()))
                    {
                        faturamento.Descontos = (faturamento.SubTotal * (porcentagemDesconto / 100));
                        faturamento.ValorTotal = (faturamento.SubTotal - faturamento.Descontos.Value);
                    }
                    else
                    {
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
    }
}
