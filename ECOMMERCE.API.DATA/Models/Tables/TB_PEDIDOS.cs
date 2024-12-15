using System.ComponentModel.DataAnnotations;

namespace ECOMMERCE.API.DATA.Models.Tables
{
    public partial class TB_PEDIDOS
    {
        [Key]
        public int PEDIDO_ID { get; set; }

        public int USUARIO_INSERCAO_ID { get; set; }

        public DateTime DATA_INSERCAO { get; set; }

        public int? USUARIO_ATUALIZACAO_ID { get; set; }

        public DateTime? DATA_ATUALIZACAO { get; set; }

        public DateTime DATA_VENDA { get; set; }

        public required string CLIENTE_ID { get; set; }

        public int STATUS_PEDIDO_ID { get; set; }
    }
}
