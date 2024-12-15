using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ECOMMERCE.API.DATA.Models.Tables
{
    public partial class TB_USUARIOS
    {
        [Key]
        public int USUARIO_ID { get; set; }

        [StringLength(255)]
        [Unicode(false)]
        public required string NOME { get; set; }

        [StringLength(255)]
        [Unicode(false)]
        public required string EMAIL { get; set; }
    }
}
