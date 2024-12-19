namespace ECOMMERCE.API.Models.DTO
{
    public class FaturamentoDTO
    {
        public int? FaturamentoId { get; set; }

        public string? PedidoId { get; set; }
        
        public decimal SubTotal { get; set; }

        public decimal? Descontos { get; set; }

        public decimal ValorTotal { get; set; }
    }
}
