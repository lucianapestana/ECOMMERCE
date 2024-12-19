namespace ECOMMERCE.API.Models
{
    public class ItensPedido
    {
        public int ItemPedidoId { get; set; }

        public required string PedidoId { get; set; }

        public int ProdutoId { get; set; }

        public string? Descricao { get; set; }

        public int Quantidade { get; set; }

        public decimal PrecoUnitario { get; set; }
    }
}
