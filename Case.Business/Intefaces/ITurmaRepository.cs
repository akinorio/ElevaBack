using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CaseElite.Business.Models;

namespace CaseElite.Business.Intefaces
{
    public interface ITurmaRepository : IRepository<Turma>
    {
        Task<IEnumerable<Turma>> ObterTurmasPorEscola(Guid escolaId);
        Task<IEnumerable<Turma>> ObterTurmasEscolas();
        Task<Turma> ObterTurmaEscola(Guid id);
    }
}