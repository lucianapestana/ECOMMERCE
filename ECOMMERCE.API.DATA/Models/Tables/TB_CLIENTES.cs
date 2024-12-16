using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ECOMMERCE.API.DATA.Models.Tables
{
    public partial class TB_CLIENTES
    {
        [Key]
        [StringLength(100)]
        public required string CLIENTE_ID { get; set; }

        public bool ATIVO { get; set; }

        public int USUARIO_INSERCAO_ID { get; set; }

        public DateTime DATA_INSERCAO { get; set; }

        public int? USUARIO_ATUALIZACAO_ID { get; set; }

        public DateTime? DATA_ATUALIZACAO { get; set; }

        [StringLength(255)]
        [Unicode(false)]
        public required string NOME { get; set; }

        [StringLength(20)]
        [Unicode(false)]
        public required string CPF { get; set; }

        public int? CATEGORIA_ID { get; set; }
    }
}
