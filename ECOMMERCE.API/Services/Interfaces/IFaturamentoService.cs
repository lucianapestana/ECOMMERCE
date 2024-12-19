using ECOMMERCE.API.Models.Api;
using ECOMMERCE.API.Models.DTO;

namespace ECOMMERCE.API.Services.Interfaces
{
    public interface IFaturamentoService
    {
        Task<Output> ProcessarFaturamento(ProcessarFaturamentoDTO dto);
    }
}
