using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ECOMMERCE.API.Models.DTO
{
    public class PedidoDTO
    {
        public int PedidoId { get; set; }

        public int UsuarioInsercaoId { get; set; }

        public int? UsuarioAtualizacaoId { get; set; }

        public int StatusPedidoId { get; set; }

        public string? Observacao { get; set; }
    }
}
