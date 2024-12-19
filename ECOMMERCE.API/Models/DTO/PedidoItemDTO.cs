namespace ECOMMERCE.API.Models.DTO
{
    public class PedidoItemDTO
    {
        public string? Identificador { get; set; }
        
        public DateTime? DataVenda { get; set; }

        public int? StatusPedido { get; set; }
        
        public ClienteDTO? Cliente { get; set; }
        
        public List<ItemPedidoDTO>? Itens { get; set; }
    }
}
