using ECOMMERCE.API.Models.Api;

namespace ECOMMERCE.API.Models.Outputs
{
    public class PedidoVendaOutput : Output
    {
        public List<Pedido>? Pedidos { get; set; }
    }
}
