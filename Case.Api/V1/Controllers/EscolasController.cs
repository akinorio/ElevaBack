using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CaseElite.Api.Controllers;
using CaseElite.Api.Extensions;
using CaseElite.Api.ViewModels;
using CaseElite.Business.Intefaces;
using CaseElite.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaseElite.Api.V1.Controllers
{
    //[Authorize]
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/escolas")]
    public class EscolasController : MainController
    {
        private readonly IEscolaRepository _escolaRepository;
        private readonly IEscolaService _escolaService;
        private readonly IMapper _mapper;

        public EscolasController(IEscolaRepository escolaRepository,
                                      IMapper mapper,
                                      IEscolaService escolaService,
                                      INotificador notificador,
                                      IUser user) : base(notificador, user)
        {
            _escolaRepository = escolaRepository;
            _mapper = mapper;
            _escolaService = escolaService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<EscolaViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<EscolaViewModel>>(await _escolaRepository.ObterTodos());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EscolaViewModel>> ObterPorId(Guid id)
        {
            var escola = await ObterEscolaTurma(id);

            if (escola == null) return NotFound();

            return escola;
        }

        private async Task<EscolaViewModel> ObterEscolaTurma(Guid id)
        {
            return _mapper.Map<EscolaViewModel>(await _escolaRepository.ObterTurmasPorEscola(id));
        }

        //[ClaimsAuthorize("Escola", "Adicionar")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<EscolaViewModel>> Adicionar(EscolaViewModel escolaViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _escolaService.Adicionar(_mapper.Map<Escola>(escolaViewModel));

            return CustomResponse(escolaViewModel);
        }

        //[ClaimsAuthorize("Escola", "Atualizar")]
        [AllowAnonymous]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<EscolaViewModel>> Atualizar(Guid id, EscolaViewModel escolaViewModel)
        {
            if (id != escolaViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(escolaViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _escolaService.Atualizar(_mapper.Map<Escola>(escolaViewModel));

            return CustomResponse(escolaViewModel);
        }

        //[ClaimsAuthorize("Escola", "Excluir")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<EscolaViewModel>> Excluir(Guid id)
        {
            var escola = await ObterPorId(id);

            if (escola == null) return NotFound();

            await _escolaService.Remover(id);

            return CustomResponse(escola);
        }

    }
}