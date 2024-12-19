namespace ECOMMERCE.API.Models.DTO
{
    public class PedidoDTO
    {
        public string? PedidoId { get; set; }

        public int UsuarioInsercaoId { get; set; }

        public int? UsuarioAtualizacaoId { get; set; }

        public DateTime DataVenda { get; set; }

        public string? ClienteId { get; set; }

        public int StatusPedidoId { get; set; }
    }
}
