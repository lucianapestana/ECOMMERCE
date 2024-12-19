using ECOMMERCE.API.Models.Api;
using ECOMMERCE.API.Models.DTO;
using ECOMMERCE.API.Services;
using ECOMMERCE.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECOMMERCE.API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/faturamento")]
    public class FaturamentoController : Controller
    {
        private readonly Output _output;
        private readonly IFaturamentoService _faturamentoService;

        public FaturamentoController(IFaturamentoService faturamentoService)
        {
            _output = new Output();
            _faturamentoService = faturamentoService;
        }

        /// <summary>
        /// Processa um faturamento.
        /// </summary>
        /// <param name="faturamento">Exemplo do objeto de requisição:</param>
        /// <response code="200">Retorno de processamento executado com sucesso.</response>
        /// <response code="400">Retorno de erro de processamento.</response>
        /// <response code="500">Retorno de servidor indisponível.</response>
        /// <returns></returns>
        [HttpPost("processar")]
        public async Task<ActionResult> ProcessarFaturamento([FromBody] ProcessarFaturamentoDTO faturamento)
        {
            try
            {

                var result = await _faturamentoService.ProcessarFaturamento(faturamento);

                _output.Status = result.Codigo == "201" ? "sucesso" : "erro";
                _output.Codigo = result.Codigo;
                _output.MensagemCodigo = result.MensagemCodigo;

                return await Task.FromResult(StatusCode(Convert.ToInt32(_output.Codigo), _output));
            }
            catch (Exception)
            {
                _output.Status = "erro";
                _output.Codigo = "500";
                _output.MensagemCodigo = "Ocorreu um erro inesperado ao processar a requisição";
                return await Task.FromResult(StatusCode(500, _output));
            }
        }
    }
}
