using System.Collections.Generic;


namespace Scire.Arquivos.Infra
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