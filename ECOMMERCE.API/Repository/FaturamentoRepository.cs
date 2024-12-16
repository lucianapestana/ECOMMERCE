using ECOMMERCE.API.DATA.Context;
using ECOMMERCE.API.DATA.Models.Tables;
using ECOMMERCE.API.Models.DTO;
using ECOMMERCE.API.Repository.Interfaces;

namespace ECOMMERCE.API.Repository
{
    public class FaturamentoRepository(EcommerceContext _context) : IFaturamentoRepository
    {
        private readonly EcommerceContext _context = _context;

        public async Task<bool> AdicionarFaturamentoVenda(FaturamentoDTO dto)
        {
            try
            {
                var faturamento = new TB_FATURAMENTOS
                {
                    PEDIDO_ID = dto.PedidoId,
                    SUB_TOTAL = dto.SubTotal,
                    DESCONTOS = dto.Descontos,
                    VALOR_TOTAL = dto.ValorTotal
                };

                _context.TB_FATURAMENTOS.Add(faturamento);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar o registro", ex);
            }
        }
    }
}
