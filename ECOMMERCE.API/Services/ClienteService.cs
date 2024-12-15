using ECOMMERCE.API.Models.Api;
using ECOMMERCE.API.Models.DTO;
using ECOMMERCE.API.Repository.Interfaces;
using ECOMMERCE.API.Services.Interfaces;

namespace ECOMMERCE.API.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<bool> AdicionarCliente(ClienteDTO dto)
        {
            try
            {
                return await _clienteRepository.AdicionarCliente(dto);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar o registro", ex);
            }
        }

        public async Task<bool> AtualizarCliente(ClienteDTO dto)
        {
            try
            {
                return await _clienteRepository.AtualizarCliente(dto);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar o registro", ex);
            }
        }

        public List<Error> ValidarCliente(ClienteDTO dto, bool adicionar = false)
        {
            var errors = new List<Error>();

            try
            {
                if (adicionar)
                {
                    if (!dto.UsuarioInsercaoId.HasValue)
                    {
                        errors.Add(new Error()
                        {
                            Objeto = "UsuarioInsercaoId",
                            Mensagem = "O campo UsuarioInsercaoId é obrigatório."
                        });
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(dto.ClienteId))
                    {
                        errors.Add(new Error()
                        {
                            Objeto = "ClienteId",
                            Mensagem = "O campo ClienteId é obrigatório."
                        });
                    }

                    if (!dto.Ativo.HasValue)
                    {
                        errors.Add(new Error()
                        {
                            Objeto = "Ativo",
                            Mensagem = "O campo Ativo é obrigatório."
                        });
                    }

                    if (!dto.UsuarioAtualizacaoId.HasValue)
                    {
                        errors.Add(new Error()
                        {
                            Objeto = "UsuarioAtualizacaoId",
                            Mensagem = "O campo UsuarioAtualizacaoId é obrigatório."
                        });
                    }
                }

                return errors;
            }
            catch (Exception ex)
            {
                errors.Add(new Error() { Objeto = "Cliente", Mensagem = $"Errro durante a validação de entrada: {ex.Message}" });
                return errors;
            }
        }
    }
}
