using ECOMMERCE.API.Models.Api;
using ECOMMERCE.API.Models.DTO;
using ECOMMERCE.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECOMMERCE.API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/cliente")]
    public class ClienteController : Controller
    {
        private readonly Output _output;
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _output = new Output();
            _clienteService = clienteService;
        }

        /// <summary>
        /// Adiciona um novo cliente.
        /// </summary>
        /// <param name="cliente">Exemplo do objeto de requisição:</param>
        /// <response code="200">Retorno de processamento executado com sucesso.</response>
        /// <response code="400">Retorno de erro de processamento.</response>
        /// <response code="500">Retorno de servidor indisponível.</response>
        /// <returns></returns>
        [HttpPost("")]
        public async Task<ActionResult> AdicionarCliente([FromBody] ClienteDTO cliente)
        {
            try
            {
                List<Error> errors = _clienteService.ValidarCliente(cliente, true);

                if (errors.Count > 0)
                {
                    _output.Status = "erro";
                    _output.Erros = errors;

                    return await Task.FromResult(StatusCode(400, _output));
                }

                var result = await _clienteService.AdicionarCliente(cliente);

                if (result)
                {
                    _output.Status = "sucesso";
                    _output.Codigo = "200";
                    _output.MensagemCodigo = "Cliente adicionado com sucesso.";
                    return await Task.FromResult(StatusCode(200, _output));
                }
                else
                {
                    _output.Status = "erro";
                    _output.Codigo = "400";
                    _output.MensagemCodigo = "Erro ao adicionar o cliente.";
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
        /// Atualiza um cliente existente.
        /// </summary>
        /// <param name="cliente">Exemplo do objeto de requisição:</param>
        /// <response code="200">Retorno de processamento executado com sucesso.</response>
        /// <response code="400">Retorno de erro de processamento.</response>
        /// <response code="500">Retorno de servidor indisponível.</response>
        /// <returns></returns>
        [HttpPut("")]
        public async Task<ActionResult> AtualizarCliente([FromBody] ClienteDTO cliente)
        {
            try
            {
                List<Error> errors = _clienteService.ValidarCliente(cliente, false);

                if (errors.Count > 0)
                {
                    _output.Status = "erro";
                    _output.Erros = errors;

                    return await Task.FromResult(StatusCode(400, _output));
                }

                var result = await _clienteService.AtualizarCliente(cliente);

                if (result)
                {
                    _output.Status = "sucesso";
                    _output.Codigo = "200";
                    _output.MensagemCodigo = "Cliente atualizado com sucesso.";
                    return await Task.FromResult(StatusCode(200, _output));
                }
                else
                {
                    _output.Status = "erro";
                    _output.Codigo = "400";
                    _output.MensagemCodigo = "Erro ao atualizar o cliente.";
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
