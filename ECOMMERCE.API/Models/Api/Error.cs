using System.ComponentModel.DataAnnotations;

namespace ECOMMERCE.API.Models.Api
{
    public class Error
    {
        [MinLength(1), MaxLength(1024)]
        public string? Objeto { get; set; }

        public string? Mensagem { get; set; }
    }
}
