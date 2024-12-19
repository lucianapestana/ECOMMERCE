namespace ECOMMERCE.API.Models
{
    public class Pedido
    {
        public Pedido()
        {
            ItensPedido = new List<ItensPedido>();
        }

        public required string PedidoId { get; set; }

        public int UsuarioInsercaoId { get; set; }

        public int? UsuarioAtualizacaoId { get; set; }

        public DateTime DataVenda { get; set; }

        public string? ClienteId { get; set; }

        public Cliente? Cliente { get; set; }

        public int StatusPedidoId { get; set; }

        public StatusPedido? StatusPedido {get; set;}

        public Faturamento? Faturamento { get; set; }

        public List<ItensPedido> ItensPedido {  get; set; }
    }
}
