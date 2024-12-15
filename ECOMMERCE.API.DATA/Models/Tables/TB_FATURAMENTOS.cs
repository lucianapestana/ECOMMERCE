using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECOMMERCE.API.DATA.Models.Tables
{
    public partial class TB_FATURAMENTOS
    {
        [Key]
        public int FATURAMENTO_ID { get; set; }

        public int PEDIDO_ID { get; set; }

        [Column(TypeName = "NUMERIC(15, 2)")]
        public decimal SUB_TOTAL { get; set; }

        [Column(TypeName = "NUMERIC(15, 2)")]
        public decimal? DESCONTOS { get; set; }

        [Column(TypeName = "NUMERIC(15, 2)")]
        public decimal VALOR_TOTAL { get; set; }
    }
}
