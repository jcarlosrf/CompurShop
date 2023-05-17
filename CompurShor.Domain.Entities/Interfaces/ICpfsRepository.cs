using CompurShop.Domain.Entities;
using System.Collections.Generic;


namespace CompurShop.Domain.Interfaces
{
    public interface ICpfsRepository
    {
        int GetQtdeCpfLista(int idlista);

        List<Cpf> GetCpfLista(int idlista);

        int GetQtdeCpf(string cpf);
        int SaveCPFS(string cpfs, int idlista);
        int GetQtdeCpfInFile(string cpfFileContent);
    }
}