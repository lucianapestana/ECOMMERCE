using System.ComponentModel.DataAnnotations;

namespace ECOMMERCE.API.DATA.Models.Tables
{
    public partial class TB_ITENS_PEDIDO
    {
        [Key]
        public int ITEM_PEDIDO_ID { get; set; }

        public required string PEDIDO_ID { get; set; }

        public int PRODUTO_ID { get; set; }

        public string DESCRICAO { get; set; }

        public int QUANTIDADE { get; set; }

        public decimal PRECO_UNITARIO { get; set; }
    }
}
