using System.ComponentModel;

namespace ECOMMERCE.API.Models.Enums
{
    public enum StatusPedidoEnum
    {
        [Description("ABERTO")]
        ABERTO = 1,

        [Description("CANCELADO")]
        CANCELADO = 2,

        [Description("CONCLUIDO")]
        CONCLUIDO = 3,
    }
}
