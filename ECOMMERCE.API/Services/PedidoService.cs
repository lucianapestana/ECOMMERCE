using ECOMMERCE.API.Models.DTO;
using ECOMMERCE.API.Repository.Interfaces;
using ECOMMERCE.API.Services.Interfaces;

namespace ECOMMERCE.API.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<bool> AdicionarPedidoItens(PedidoItemDTO dto)
        {
            try
            {
                return await _pedidoRepository.AdicionarPedidoItens(dto);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar o registro", ex);
            }
        }


    }
}
