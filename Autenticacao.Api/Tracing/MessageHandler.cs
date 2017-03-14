using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using log4net;

namespace Autenticacao.Api.Tracing
{
    public abstract class MessageHandler : DelegatingHandler
    {
        protected static readonly ILog Log4Net = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var corrId = Guid.NewGuid().ToString();
            var requestInfo = $"{request.Method} {request.RequestUri}";

            byte[] requestMessage = null;

            if (HttpContext.Current.Request.Files.Count < 1)
                requestMessage = await request.Content.ReadAsByteArrayAsync();

            await IncommingMessageAsync(corrId, requestInfo, requestMessage);


            request.Content.Headers.Add("IdRequisicao", corrId);

            var response = await base.SendAsync(request, cancellationToken);

            byte[] responseMessage;

            if (response.IsSuccessStatusCode)
                responseMessage = response.Content == null ? null : await response.Content.ReadAsByteArrayAsync();
            else
                responseMessage = Encoding.UTF8.GetBytes($"StatusCode: {Convert.ToInt32(response.StatusCode)} - {response.ReasonPhrase}");

            await OutgoingMessageAsync(corrId, requestInfo, responseMessage);
            return response;
        }


        protected abstract Task IncommingMessageAsync(string correlationId, string requestInfo, byte[] message);
        protected abstract Task OutgoingMessageAsync(string correlationId, string requestInfo, byte[] message);
    }
}