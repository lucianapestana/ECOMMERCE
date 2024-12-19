namespace ECOMMERCE.API.Models
{
    public class Faturamento
    {
        public int? FaturamentoId { get; set; }

        public required string PedidoId { get; set; }

        public decimal SubTotal { get; set; }

        public decimal? Descontos { get; set; }

        public decimal ValorTotal { get; set; }
    }
}
