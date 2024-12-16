using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ECOMMERCE.API.Models.DTO
{
    public class ClienteDTO
    {
        [MaxLength(100, ErrorMessage = "Limite da propriedade {0} ultrapassa o valor máximo de {1} caracteres.")]
        public string? ClienteId { get; set; }

        public bool? Ativo { get; set; }

        public int? UsuarioInsercaoId { get; set; }

        public int? UsuarioAtualizacaoId { get; set; }
        
        [MaxLength(255, ErrorMessage = "Limite da propriedade {0} ultrapassa o valor máximo de {1} caracteres.")]
        public required string Nome { get; set; }

        [MaxLength(20, ErrorMessage = "Limite da propriedade {0} ultrapassa o valor máximo de {1} caracteres.")]
        public required string? Cpf { get; set; }

        public int? Categoria { get; set; }
    }
}
