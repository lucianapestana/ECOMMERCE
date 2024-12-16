using ECOMMERCE.API.Models;

namespace ECOMMERCE.API.Repository.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> ObterTodas(int? categoriaId = null);
    }
}
