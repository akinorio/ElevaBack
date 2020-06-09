using System;
using System.Threading.Tasks;
using CaseElite.Business.Models;

namespace CaseElite.Business.Intefaces
{
    public interface IEscolaService : IDisposable
    {
        Task<bool> Adicionar(Escola escola);
        Task<bool> Atualizar(Escola escola);
        Task<bool> Remover(Guid id);
    }
}