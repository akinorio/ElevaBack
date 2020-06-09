using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using CaseElite.Api.Controllers;
using CaseElite.Api.Extensions;
using CaseElite.Api.ViewModels;
using CaseElite.Business.Intefaces;
using CaseElite.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseElite.Api.V1.Controllers
{
    //[Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/turmas")]
    public class TurmaController : MainController
    {
        private readonly ITurmaRepository _turmaRepository;
        private readonly ITurmaService _turmaService;
        private readonly IMapper _mapper;

        public TurmaController(INotificador notificador,
                                  ITurmaRepository turmaRepository,
                                  ITurmaService turmaService,
                                  IMapper mapper,
                                  IUser user) : base(notificador, user)
        {
            _turmaRepository = turmaRepository;
            _turmaService = turmaService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<TurmaViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<TurmaViewModel>>(await _turmaRepository.ObterTurmasEscolas());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TurmaViewModel>> ObterPorId(Guid id)
        {
            var turmaViewModel = await ObterTurma(id);

            if (turmaViewModel == null) return NotFound();

            return turmaViewModel;
        }

        //[ClaimsAuthorize("Turma", "Adicionar")]
        [HttpPost]
        public async Task<ActionResult<TurmaViewModel>> Adicionar(TurmaViewModel turmaViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _turmaService.Adicionar(_mapper.Map<Turma>(turmaViewModel));

            return CustomResponse(turmaViewModel);
        }

        //[ClaimsAuthorize("Turma", "Atualizar")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, TurmaViewModel turmaViewModel)
        {
            if (id != turmaViewModel.Id)
            {
                NotificarErro("Os ids informados não são iguais!");
                return CustomResponse();
            }

            var turmaAtualizacao = await ObterTurma(id);

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            turmaAtualizacao.EscolaId = turmaViewModel.EscolaId;
            turmaAtualizacao.Nome = turmaViewModel.Nome;
            turmaAtualizacao.Descricao = turmaViewModel.Descricao;
            turmaAtualizacao.Ativo = turmaViewModel.Ativo;

            await _turmaService.Atualizar(_mapper.Map<Turma>(turmaAtualizacao));

            return CustomResponse(turmaViewModel);
        }

        //[ClaimsAuthorize("Turma", "Excluir")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<TurmaViewModel>> Excluir(Guid id)
        {
            var turma = await ObterTurma(id);

            if (turma == null) return NotFound();

            await _turmaService.Remover(id);

            return CustomResponse(turma);
        }

        private async Task<TurmaViewModel> ObterTurma(Guid id)
        {
            return _mapper.Map<TurmaViewModel>(await _turmaRepository.ObterTurmaEscola(id));
        }

    }
}