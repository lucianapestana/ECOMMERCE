namespace ECOMMERCE.API.Models
{
    public class Cliente
    {
        public required string ClienteId { get; set; }

        public bool Ativo { get; set; }

        public int UsuarioInsercaoId { get; set; }

        public DateTime DataInsercao { get; set; }

        public int? UsuarioAtualizacaoId { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        public required string Nome { get; set; }

        public required string Cpf { get; set; }

        public int? CategoriaId { get; set; }
    }
}
