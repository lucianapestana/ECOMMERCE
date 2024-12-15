using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ECOMMERCE.API.DATA.Models.Tables
{
    public partial class TB_CATEGORIAS
    {
        [Key]
        public int CATEGORIA_ID { get; set; }

        public bool ATIVO { get; set; }

        public int USUARIO_INSERCAO_ID { get; set; }

        public DateTime DATA_INSERCAO { get; set; }

        public int? USUARIO_ATUALIZACAO_ID { get; set; }

        public DateTime? DATA_ATUALIZACAO { get; set; }

        [StringLength(255)]
        [Unicode(false)]
        public required string DESCRICAO { get; set; }

        [Column(TypeName = "NUMERIC(15, 2)")]
        public decimal PORCENTAGEM_DESCONTO { get; set; }
    }
}
