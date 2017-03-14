using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Autenticacao.Api.Tracing
{
    public class MessageLoggingHandler : MessageHandler
    {
        protected override async Task IncommingMessageAsync(string correlationId, string requestInfo, byte[] message)
        {
            await Task.Run(() =>
            {

                var msg = message != null && message.Length > 0
                    ? $"{Encoding.UTF8.GetString(message)}"
                    : string.Empty;

                GlobalContext.Properties["Conteudo"] = msg;
                GlobalContext.Properties["RequestId"] = correlationId;

                Log4Net.InfoFormat("Request - IdRequisicao: {0} - Informações: {1}{2}", correlationId,
                    requestInfo,
                    msg);

                
            });
        }


        protected override async Task OutgoingMessageAsync(string correlationId, string requestInfo, byte[] message)
        {
            await Task.Run(() =>
            {
                var msg = message != null && message.Length > 0
                    ? $"{Encoding.UTF8.GetString(message)}"
                    : string.Empty;
                GlobalContext.Properties["Conteudo"] = msg;
                GlobalContext.Properties["RequestId"] = correlationId;

                Log4Net.InfoFormat("Response - IdRequisicao: {0} - Informações: {1}{2}", correlationId,
                    requestInfo,
                    msg);
            });
        }
    }
}