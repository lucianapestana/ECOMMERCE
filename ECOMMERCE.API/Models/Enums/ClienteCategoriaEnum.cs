using System.ComponentModel;

namespace ECOMMERCE.API.Models.Enums
{
    public enum ClienteCategoriaEnum
    {
        [Description("REGULAR")]
        REGULAR = 1,

        [Description("PREMIUM")]
        PREMIUM = 2,

        [Description("VIP")]
        VIP = 3
    }
}
