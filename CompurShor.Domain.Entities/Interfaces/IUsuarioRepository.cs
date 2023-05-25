using CompurShop.Domain.Entities;

namespace CompurShop.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        int Save(Usuario usuario);

        Usuario GetEntity(int id);

        Usuario GetByName(string nome);

        int Delete(int id);
    }
}
