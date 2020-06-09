using System;
using System.Threading.Tasks;
using CaseElite.Business.Models;

namespace CaseElite.Business.Intefaces
{
    public interface ITurmaService : IDisposable
    {
        Task Adicionar(Turma turma);
        Task Atualizar(Turma turma);
        Task Remover(Guid id);
    }
}