using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CaseElite.Business.Intefaces;
using CaseElite.Business.Models;
using CaseElite.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CaseElite.Data.Repository
{
    public class EscolaRepository : Repository<Escola>, IEscolaRepository
    {
        public EscolaRepository(MeuDbContext context) : base(context)
        {
        }

        public async Task<Escola> ObterTurmasPorEscola(Guid escolaId)
        {
            return await Db.Escolas.AsNoTracking()
                .Include(c => c.Turmas)
                .FirstOrDefaultAsync(c => c.Id == escolaId);
        }

        public async Task<Escola> ObterEscola(Guid id)
        {
            return await Db.Escolas.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

    }
}