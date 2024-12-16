using ECOMMERCE.API.DATA.Context;
using ECOMMERCE.API.Models;
using ECOMMERCE.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECOMMERCE.API.Repository
{
    public class CategoriaRepository(EcommerceContext _context) : ICategoriaRepository
    {
        private readonly EcommerceContext _context = _context;

        public async Task<IEnumerable<Categoria>> ObterTodas(int? categoriaId = null)
        {
            try
            {
                var result = await (
                    from categoria in _context.TB_CATEGORIAS
                    where
                    (
                        (
                            categoriaId.HasValue
                            && categoria.CATEGORIA_ID == categoriaId
                        ) || !categoriaId.HasValue
                    )
                    select new Categoria()
                    {
                        CategoriaId = categoria.CATEGORIA_ID,
                        Ativo = categoria.ATIVO,
                        UsuarioInsercaoId = categoria.USUARIO_INSERCAO_ID,
                        DataInsercao = categoria.DATA_INSERCAO,
                        UsuarioAtualizacaoId = categoria.USUARIO_ATUALIZACAO_ID,
                        DataAtualizacao = categoria.DATA_ATUALIZACAO,
                        Descricao = categoria.DESCRICAO,
                        PorcentagemDesconto = categoria.PORCENTAGEM_DESCONTO
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
