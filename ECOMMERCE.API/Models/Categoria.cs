namespace ECOMMERCE.API.Models
{
    public class Categoria
    {
        public int CategoriaId { get; set; }

        public bool Ativo { get; set; }

        public int UsuarioInsercaoId { get; set; }

        public DateTime DataInsercao { get; set; }

        public int? UsuarioAtualizacaoId { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        public required string Descricao { get; set; }

        public decimal PorcentagemDesconto { get; set; }
    }
}
