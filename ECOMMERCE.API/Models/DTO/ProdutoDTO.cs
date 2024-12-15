using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECOMMERCE.API.Models.DTO
{
    public class ProdutoDTO
    {
        public int ProdutoId { get; set; }

        public bool Ativo { get; set; }

        public int UsuarioInsercaoId { get; set; }

        public int? UsuarioAtualizacaoId { get; set; }

        public required string Descricao { get; set; }

        public decimal PrecoUnitario { get; set; }
    }
}
