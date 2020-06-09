using System.Collections.Generic;
using CaseElite.Business.Notificacoes;

namespace CaseElite.Business.Intefaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}