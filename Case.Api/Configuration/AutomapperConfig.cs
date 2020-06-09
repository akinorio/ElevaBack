using AutoMapper;
using CaseElite.Api.ViewModels;
using CaseElite.Business.Models;

namespace CaseElite.Api.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Escola, EscolaViewModel>().ReverseMap();
            CreateMap<TurmaViewModel, Turma>();

            CreateMap<Turma, TurmaViewModel>()
                .ForMember(dest => dest.NomeEscola, opt => opt.MapFrom(src => src.Escola.Nome));
        }
    }
}