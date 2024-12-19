namespace ECOMMERCE.API.Models.DTO
{
    public class ProcessarFaturamentoDTO
    {
        public string? Email { get; set; }

        public required string Identificador { get; set; }

        public decimal SubTotal { get; set; }

        public decimal? Descontos { get; set; }
        
        public decimal ValorTotal { get; set; }

        public List<ItemFaturamentoDTO>? Itens { get; set; }
    }
}
