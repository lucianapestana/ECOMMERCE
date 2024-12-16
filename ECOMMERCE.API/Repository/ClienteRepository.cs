using ECOMMERCE.API.DATA.Context;
using ECOMMERCE.API.DATA.Models.Tables;
using ECOMMERCE.API.Models;
using ECOMMERCE.API.Models.DTO;
using ECOMMERCE.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECOMMERCE.API.Repository
{
    public class ClienteRepository(EcommerceContext _context) : IClienteRepository
    {
        private readonly EcommerceContext _context = _context;

        public async Task<bool> AdicionarCliente(ClienteDTO dto)
        {
            try
            {
                var cliente = new TB_CLIENTES
                {
                    CLIENTE_ID = Guid.NewGuid().ToString().ToUpper(),
                    ATIVO = true,
                    USUARIO_INSERCAO_ID = 1,
                    DATA_INSERCAO = DateTime.Now,
                    NOME = dto.Nome,
                    CPF = dto.Cpf,
                    CATEGORIA_ID = dto.Categoria
                };

                _context.TB_CLIENTES.Add(cliente);
                return await _context.SaveChangesAsync() > 0;
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
                var clienteExistente = _context.TB_CLIENTES.Find(dto.ClienteId);

                if (clienteExistente != null)
                {
                    clienteExistente.ATIVO = dto.Ativo.Value;
                    clienteExistente.USUARIO_ATUALIZACAO_ID = dto.UsuarioAtualizacaoId;
                    clienteExistente.DATA_ATUALIZACAO = DateTime.Now;
                    clienteExistente.NOME = dto.Nome;
                    clienteExistente.CPF = dto.Cpf;
                    clienteExistente.CATEGORIA_ID = dto.Categoria;
                    
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar o registro", ex);
            }
        }

        public async Task<IEnumerable<Cliente>> ObterTodos(string? clienteId = null)
        {
            try
            {
                var result = await (
                    from cliente in  _context.TB_CLIENTES
                    where 
                    (
                        (
                            !string.IsNullOrWhiteSpace(clienteId)
                            && cliente.CLIENTE_ID == clienteId
                        ) || string.IsNullOrWhiteSpace(clienteId)
                    )
                    select new Cliente()
                    { 
                        ClienteId = cliente.CLIENTE_ID,
                        Ativo = cliente.ATIVO,
                        UsuarioInsercaoId = cliente.USUARIO_INSERCAO_ID,
                        DataInsercao = cliente.DATA_INSERCAO,
                        UsuarioAtualizacaoId = cliente.USUARIO_ATUALIZACAO_ID,
                        DataAtualizacao = cliente.DATA_ATUALIZACAO,
                        Nome = cliente.NOME,
                        Cpf = cliente.CPF,
                        CategoriaId = cliente.CATEGORIA_ID
                    }
                    ).ToListAsync();
                
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao obter os registros", ex);
            }
        }
    }
}
