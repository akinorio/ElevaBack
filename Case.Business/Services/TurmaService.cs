using System;
using System.Threading.Tasks;
using CaseElite.Business.Intefaces;
using CaseElite.Business.Models;
using CaseElite.Business.Models.Validations;

namespace CaseElite.Business.Services
{
    public class TurmaService : BaseService, ITurmaService
    {
        private readonly ITurmaRepository _turmaRepository;
        private readonly IUser _user;

        public TurmaService(ITurmaRepository turmaRepository,
                              INotificador notificador,
                              IUser user) : base(notificador)
        {
            _turmaRepository = turmaRepository;
            _user = user;
        }

        public async Task Adicionar(Turma turma)
        {
            if (!ExecutarValidacao(new TurmaValidation(), turma)) return;

            //var user = _user.GetUserId();

            await _turmaRepository.Adicionar(turma);
        }

        public async Task Atualizar(Turma turma)
        {
            if (!ExecutarValidacao(new TurmaValidation(), turma)) return;

            await _turmaRepository.Atualizar(turma);
        }

        public async Task Remover(Guid id)
        {
            await _turmaRepository.Remover(id);
        }

        public void Dispose()
        {
            _turmaRepository?.Dispose();
        }
    }
}