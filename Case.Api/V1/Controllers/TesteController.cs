using CaseElite.Api.Controllers;
using CaseElite.Business.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace CaseElite.Api.V1.Controllers
{
    [ApiVersion("1.1", Deprecated = true)]
    [Route("api/v{version:apiVersion}/teste")]
    public class TesteController : MainController
    {

        public TesteController(INotificador notificador, IUser appUser) : base(notificador, appUser)
        {
        }

        [HttpGet]
        public string Valor()
        {
            return "Sou a V1";
        }
    }
}