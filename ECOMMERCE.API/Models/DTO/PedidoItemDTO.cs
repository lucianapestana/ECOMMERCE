namespace ECOMMERCE.API.Models.DTO
{
    public class PedidoItemDTO
    {
        public required string Identificador { get; set; }
        
        public DateTime DataVenda { get; set; }
        
        public required ClienteDTO Cliente { get; set; }
        
        public required List<ItemPedidoDTO> Itens { get; set; }
    }
}
