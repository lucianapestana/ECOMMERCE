namespace ECOMMERCE.API.Models.Api
{
    public class Output
    {
        public string Status { get; set; }

        public string Codigo { get; set; }

        public string MensagemCodigo { get; set; }

        public List<Error> Erros { get; set; }
    }
}
