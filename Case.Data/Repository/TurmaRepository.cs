using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseElite.Business.Intefaces;
using CaseElite.Business.Models;
using CaseElite.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CaseElite.Data.Repository
{
    public class TurmaRepository : Repository<Turma>, ITurmaRepository
    {
        public TurmaRepository(MeuDbContext context) : base(context) { }

        public async Task<Turma> ObterTurmaEscola(Guid id)
        {
            return await Db.Turmas.AsNoTracking().Include(f => f.Escola)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Turma>> ObterTurmasEscolas()
        {
            return await Db.Turmas.AsNoTracking().Include(f => f.Escola)
                .OrderBy(p => p.Nome).ToListAsync();
        }

        public async Task<IEnumerable<Turma>> ObterTurmasPorEscola(Guid escolaId)
        {
            return await Buscar(p => p.EscolaId == escolaId);
        }
    }
}