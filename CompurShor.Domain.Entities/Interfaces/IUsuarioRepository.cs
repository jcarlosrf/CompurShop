using CompurShop.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CompurShop.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        int Save(Usuario usuario);

        Usuario GetEntity(int id);

        Usuario GetByName(string nome);

        IQueryable<Usuario> GetAll();

        IQueryable<Usuario> GetbyNome(string nome);

        int Delete(int id);
    }
}
