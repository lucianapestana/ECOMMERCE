using ECOMMERCE.API.Models.DTO;
using ECOMMERCE.API.Models.Outputs;
using ECOMMERCE.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECOMMERCE.API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/pedido")]
    public class PedidoController : ControllerBase
    {
        private readonly PedidoVendaOutput _output;
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _output = new PedidoVendaOutput();
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

        /// <summary>
        /// Altera pedido somente com status ABERTO.
        /// </summary>
        /// <param name="pedidoItem">Exemplo do objeto de requisição:</param>
        /// <response code="200">Retorno de processamento executado com sucesso.</response>
        /// <response code="400">Retorno de erro de processamento.</response>
        /// <response code="500">Retorno de servidor indisponível.</response>
        /// <returns></returns>
        [HttpPut("itens")]
        public async Task<ActionResult> AtualizarPedidoItens([FromBody] PedidoItemDTO pedidoItem)
        {
            try
            {
                var output = await _pedidoService.AtualizarPedidoItens(pedidoItem);

                _output.Codigo = output.Codigo;
                _output.MensagemCodigo = output.MensagemCodigo;

                if (output.Codigo == "200")
                {
                    _output.Status = "sucesso";
                    return await Task.FromResult(StatusCode(200, _output));
                }
                else
                {
                    _output.Status = "erro";
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

        /// <summary>
        /// Lista todos os pedidos.
        /// </summary>
        /// <response code="200">Retorno de processamento executado com sucesso.</response>
        /// <response code="400">Retorno de erro de processamento.</response>
        /// <response code="500">Retorno de servidor indisponível.</response>
        /// <returns></returns>
        [HttpGet("listar")]
        public async Task<ActionResult> ListarPedidos()
        {
            try
            {
                var result = await _pedidoService.ObterPedidos();

                if (result.Pedidos != null && result.Pedidos.Count > 0)
                {
                    _output.Status = "sucesso";
                    _output.Codigo = "200";
                    _output.MensagemCodigo = "Pedidos listados com sucesso.";
                    _output.Pedidos = result.Pedidos;
                    return await Task.FromResult(StatusCode(200, _output));
                }
                else
                {
                    _output.Status = "erro";
                    _output.Codigo = "400";
                    _output.MensagemCodigo = "Nemhum pedido encontrado.";
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

        /// <summary>
        /// Obtem pedido pelo ID.
        /// </summary>
        /// <param name="id">Exemplo do objeto de requisição:</param>
        /// <response code="200">Retorno de processamento executado com sucesso.</response>
        /// <response code="400">Retorno de erro de processamento.</response>
        /// <response code="500">Retorno de servidor indisponível.</response>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> ListarPedidos(string id)
        {
            try
            {
                var result = await _pedidoService.ObterPedidos(id);

                if (result.Pedidos != null && result.Pedidos.Count > 0)
                {
                    _output.Status = "sucesso";
                    _output.Codigo = "200";
                    _output.MensagemCodigo = "Pedido obtido com sucesso.";
                    _output.Pedidos = result.Pedidos;
                    return await Task.FromResult(StatusCode(200, _output));
                }
                else
                {
                    _output.Status = "erro";
                    _output.Codigo = "400";
                    _output.MensagemCodigo = $"Pedido {id} não existe na nossa base de dados.";
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
