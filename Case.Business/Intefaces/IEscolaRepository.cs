using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CaseElite.Business.Models;

namespace CaseElite.Business.Intefaces
{
    public interface IEscolaRepository : IRepository<Escola>
    {
        Task<Escola> ObterEscola(Guid id);
        Task<Escola> ObterTurmasPorEscola(Guid id);
    }
}