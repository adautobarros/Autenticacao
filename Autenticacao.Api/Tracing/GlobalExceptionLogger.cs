using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using Autenticacao.Api.Helpers;
using log4net;

namespace Autenticacao.Api.Tracing
{
    public class GlobalExceptionLogger : ExceptionLogger
    {
        private static readonly ILog Log4Net = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            StringBuilder mensagem = new StringBuilder();
            if (context.Request != null)
            {
                IEnumerable<string> idRequisicao;
                if (context.Request.Content != null && context.Request.Content.Headers.TryGetValues("IdRequisicao", out idRequisicao))
                {
                    mensagem.AppendFormat("IdRequisicao: {0}<br>", string.Join(",", idRequisicao));
                }

                mensagem.AppendFormat("Metodo: {0}<br/>Url: {1}<br/>Headers: {2}<br/>", context.Request.Method,
                  context.Request.RequestUri.AbsoluteUri,
                  context.Request.Headers);

                var requestMessage = context.Request.Content?.ReadAsByteArrayAsync().Result;
                if (requestMessage?.Length > 0)
                {
                    var paramentros = Encoding.UTF8.GetString(requestMessage);
                    mensagem.AppendFormat("Parametros: {0}", paramentros);
                }


                var claim = (ClaimsIdentity)context.RequestContext?.Principal.Identity;
                if (claim != null)
                {
                    var perfis = claim.Claims?.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
                    var codioUsuario = claim.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                    var emailUsuario = claim.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                    var nomeUsuario = claim.Claims?.FirstOrDefault(c => c.Type == ClaimTypesCustom.FullName)?.Value;
                    var login = context.RequestContext.Principal.Identity.Name;

                    mensagem.AppendFormat(
                        "LoginUsuario: {0}, NomeUsuario: {1}, EmailUsuario: {2}, CodigoUsuario: {3},  Perfis: {4}",
                        login, nomeUsuario, emailUsuario, codioUsuario, perfis == null ? string.Empty : string.Join(",", perfis));
                }
            }
            Log4Net.Fatal($"Oops! Algo deu errado. {mensagem}", context.Exception);

            return base.LogAsync(context, cancellationToken);
        }
    }
}