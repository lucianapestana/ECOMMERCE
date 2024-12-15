using ECOMMERCE.API.DATA.Context;
using ECOMMERCE.API.Repository.Interfaces;

namespace ECOMMERCE.API.Repository
{
    public class ProdutoRepository(EcommerceContext _context) : IProdutoRepository
    {
        private readonly EcommerceContext _context = _context;
    }
}
