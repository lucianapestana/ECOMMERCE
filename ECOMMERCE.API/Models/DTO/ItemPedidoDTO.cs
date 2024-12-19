namespace ECOMMERCE.API.Models.DTO
{
    public class ItemPedidoDTO
    {
        public int ItemPedidoId { get; set; }

        public string? PedidoId { get; set; }

        public int ProdutoId { get; set; }

        public string? Descricao { get; set; }

        public int Quantidade { get; set; }

        public decimal PrecoUnitario { get; set; }
    }
}
