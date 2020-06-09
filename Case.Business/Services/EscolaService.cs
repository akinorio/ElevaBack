using System;
using System.Linq;
using System.Threading.Tasks;
using CaseElite.Business.Intefaces;
using CaseElite.Business.Models;
using CaseElite.Business.Models.Validations;

namespace CaseElite.Business.Services
{
    public class EscolaService : BaseService, IEscolaService
    {
        private readonly IEscolaRepository _escolaRepository;
        private readonly ITurmaRepository _turmaRepository;

        public EscolaService(IEscolaRepository escolaRepository,
                                 ITurmaRepository turmaRepository,
                                 INotificador notificador) : base(notificador)
        {
            _escolaRepository = escolaRepository;
            _turmaRepository = turmaRepository;
        }

        public async Task<bool> Adicionar(Escola escola)
        {
            if (!ExecutarValidacao(new EscolaValidation(), escola)) return false;

            await _escolaRepository.Adicionar(escola);
            return true;
        }

        public async Task<bool> Atualizar(Escola escola)
        {
            if (!ExecutarValidacao(new EscolaValidation(), escola)) return false;

            await _escolaRepository.Atualizar(escola);
            return true;
        }

        public async Task AtualizarTurma(Turma turma)
        {
            if (!ExecutarValidacao(new TurmaValidation(), turma)) return;

            await _turmaRepository.Atualizar(turma);
        }

        public async Task<bool> Remover(Guid id)
        {
            if (_escolaRepository.ObterTurmasPorEscola(id).Result.Turmas.Any())
            {
                Notificar("A escola possui turmas cadastradas!");
                return false;
            }

            var turma = await _turmaRepository.ObterTurmasPorEscola(id);

            //if (turma != null)
            //{
            //    await _turmaRepository.Remover(turma.Id);
            //}

            await _escolaRepository.Remover(id);
            return true;
        }

        public void Dispose()
        {
            _escolaRepository?.Dispose();
            _turmaRepository?.Dispose();
        }
    }
}