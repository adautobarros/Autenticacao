using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Autenticacao.Api.Helpers;
using Autenticacao.Dominio.Servicos;
using Autenticacao.SharedKernel.Events;

namespace Autenticacao.Api.Tracing
{
    public class AppKeyHandle : DelegatingHandler
    {
        private readonly IAppKeyAplicacaoServico _appKeyAplicacaoServico;
        public AppKeyHandle()
        {
            _appKeyAplicacaoServico = DomainEvent.Container.GetService<IAppKeyAplicacaoServico>();
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage, CancellationToken cancellationToken)
        {
            try
            {
                if (ValidaAppKey(requestMessage))
                    return base.SendAsync(requestMessage, cancellationToken);
                return SendError("Você não pode usar a API sem a chave", HttpStatusCode.Forbidden, cancellationToken);
            }
            catch (Exception ex)
            {
                return SendError(ex.Message, HttpStatusCode.InternalServerError, cancellationToken);
            }
        }
        private Task<HttpResponseMessage> SendError(string error, HttpStatusCode code, CancellationToken cancellationToken)
        {
            return Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(code)
            {
                Content = new StringContent(error)
            }, cancellationToken);
        }
        private bool ValidaAppKey(HttpRequestMessage requestMessage)
        {
            var key = requestMessage.GetHeader("App-Key");
            if (string.IsNullOrWhiteSpace(key))
                return false;
            var appKey = _appKeyAplicacaoServico.Obter(new Guid(key));
            return appKey != null;
        }
    }
}