using ECOMMERCE.API.Models.Api;
using ECOMMERCE.API.Models.DTO;
using ECOMMERCE.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECOMMERCE.API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/pedido")]
    public class PedidoController : ControllerBase
    {
        private readonly Output _output;
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _output = new Output();
            _pedidoService = pedidoService;
        }

        /// <summary>
        /// Adiciona um novo pedido com os itens selecionados.
        /// </summary>
        /// <param name="pedidoItem">Exemplo do objeto de requisição:</param>
        /// <response code="200">Retorno de processamento executado com sucesso.</response>
        /// <response code="400">Retorno de erro de processamento.</response>
        /// <response code="500">Retorno de servidor indisponível.</response>
        /// <returns></returns>
        [HttpPost("itens")]
        public async Task<ActionResult> AdicionarPedidoItens([FromBody] PedidoItemDTO pedidoItem)
        {
            try
            {
                var result = await _pedidoService.AdicionarPedidoItens(pedidoItem);

                if (result)
                {
                    _output.Status = "sucesso";
                    _output.Codigo = "200";
                    _output.MensagemCodigo = "Pedido com os itens adicionado com sucesso.";
                    return await Task.FromResult(StatusCode(200, _output));
                }
                else
                {
                    _output.Status = "erro";
                    _output.Codigo = "400";
                    _output.MensagemCodigo = "Erro ao adicionar o pedido com os itens.";
                    return await Task.FromResult(StatusCode(400, _output));
                }
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
