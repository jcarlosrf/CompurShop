using CompurShop.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CompurShop.Domain.Interfaces
{
    public interface ICpfsRepository
    {
        IQueryable<Cpf> GetCpfLista(int idlista);

        int GetQtdeCpfLista(int idlista);

        int SaveCPFS(string cpfs, int idlista);
    }
}