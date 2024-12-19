using ECOMMERCE.API.Models.DTO;

namespace ECOMMERCE.API.Repository.Interfaces
{
    public interface IFaturamentoRepository
    {
        Task<bool> AdicionarFaturamentoVenda(FaturamentoDTO dto);

        Task<bool> AtualizarFaturamento(FaturamentoDTO dto);
    }
}
