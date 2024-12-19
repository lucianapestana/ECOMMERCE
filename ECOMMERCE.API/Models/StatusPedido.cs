using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ECOMMERCE.API.Models
{
    public class StatusPedido
    {
        public int StatusPedidoId { get; set; }

        public bool Ativo { get; set; }

        public int UsuarioInsercaoId { get; set; }

        public DateTime DataInsercao { get; set; }

        public int? UsuarioAtualizacaoId { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        public required string Descricao { get; set; }
    }
}
