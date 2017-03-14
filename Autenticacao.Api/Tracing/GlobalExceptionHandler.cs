using System.Collections.Generic;
using System.Net;

namespace Autenticacao.Api.Tracing
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void HandleCore(System.Web.Http.ExceptionHandling.ExceptionHandlerContext context)
        {

            var mensagemAdicional = string.Empty;
            IEnumerable<string> idRequisicao;
            if (context.Request.Content != null &&
                context.Request.Content.Headers.TryGetValues("IdRequisicao", out idRequisicao))
            {
                mensagemAdicional = $", por favor informe o código: {string.Join(",", idRequisicao)}";
            }

            context.Result = new GlobalException()
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = $"Oops! Desculpa! Algo deu errado. Entre em contato com tiincidentessomos@somoseducacao.com.br para que possamos corrigi-lo{mensagemAdicional}",
                Request = context.Request
            };

        }
    }
}