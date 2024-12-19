using System.Text;
using ECOMMERCE.API.Models.Api;
using ECOMMERCE.API.Models.DTO;
using ECOMMERCE.API.Models.Enums;
using ECOMMERCE.API.Repository.Interfaces;
using ECOMMERCE.API.Services.Interfaces;
using Newtonsoft.Json;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;

namespace ECOMMERCE.API.Services
{
    public class FaturamentoService : IFaturamentoService
    {
        private readonly HttpClient _httpClient;
        private readonly AsyncRetryPolicy _retryPolicy;
        private readonly AsyncCircuitBreakerPolicy _circuitBreakerPolicy;
        private readonly IPedidoRepository _pedidoRepository;

        private const string _endpointFaturamento = "https://sti3-faturamento.azurewebsites.net/api/vendas";

        public FaturamentoService
            (
                HttpClient httpClient,
                IPedidoRepository pedidoRepository
            )
        {
            _httpClient = httpClient;
            _pedidoRepository = pedidoRepository;

            _retryPolicy = Policy.Handle<HttpRequestException>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            _circuitBreakerPolicy = Policy.Handle<HttpRequestException>()
                .CircuitBreakerAsync(3, TimeSpan.FromMinutes(1));
        }

        public async Task<Output> ProcessarFaturamento(ProcessarFaturamentoDTO dto)
        {
            var output = new Output();

            if (string.IsNullOrWhiteSpace(dto.Email))
            {
                output.Codigo = "401";
                output.MensagemCodigo = "É necessário informar um e-mail para processar o faturamento.";
                return output;
            }

            var pedidos = await _pedidoRepository.ObterPedidos(dto.Identificador);

            if (pedidos != null && pedidos.First().StatusPedidoId == StatusPedidoEnum.ABERTO.GetHashCode())
            {
                try
                {
                    var header = new Tuple<string, string?>("email", dto.Email);

                    var resultado = await _retryPolicy.ExecuteAsync(async () =>
                    {
                        return await _circuitBreakerPolicy.ExecuteAsync(async () =>
                        {
                            var response = await EnviarFaturamento(_endpointFaturamento, dto, header);

                            if (response.IsSuccessStatusCode)
                            {
                                var pedido = new PedidoDTO()
                                {
                                    PedidoId = dto.Identificador,
                                    StatusPedidoId = StatusPedidoEnum.CONCLUIDO.GetHashCode()
                                };

                                var result = await _pedidoRepository.AtualizarPedido(pedido);

                                if (!result)
                                {
                                    output.Codigo = "400";
                                    output.MensagemCodigo = "Faturamento processado com sucesso, porém ocorreu um erro ao atualizar o pedido.";
                                }
                                else
                                {
                                    output.Codigo = Convert.ToInt64(response.StatusCode).ToString();
                                    output.MensagemCodigo = "Faturamento processado com sucesso.";
                                }

                                return await response.Content.ReadAsStringAsync();
                            }
                            else
                            {

                                output.Codigo = Convert.ToInt64(response.StatusCode).ToString();
                                output.MensagemCodigo = response.StatusCode.ToString();

                                throw new HttpRequestException($"{response.StatusCode} - Erro ao processar o faturamento.");
                            }
                        });
                    });
                }
                catch (BrokenCircuitException)
                {
                    output.MensagemCodigo = "Serviço temporariamente indisponível. Tente novamente mais tarde.";
                }
                catch (Exception ex)
                {
                    output.MensagemCodigo = $"Ocorreu um erro ao processar o registro: {ex.Message}";
                }
            }
            else
            {
                output.Codigo = "400";
                output.MensagemCodigo = $"O faturamento do pedido { dto.Identificador } já foi processado.";
            }

            return output;
        }

        private async Task<HttpResponseMessage> EnviarFaturamento(string endpoint, ProcessarFaturamentoDTO payload, Tuple<string, string> header)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add(header.Item1, header.Item2);

            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);
            return response;
        }
    }
}
