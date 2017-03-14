using System.Web.Http;
using Autenticacao.Api.Helpers;

namespace Autenticacao.Api.Controllers
{
    public class UsuarioHelperApiController : ApiController
    {
        protected string NomeUsuario => ClaimsIdentityHelper.NomeUsuario;
        protected int CodigoUsuario => ClaimsIdentityHelper.CodigoUsuario;
    }
}