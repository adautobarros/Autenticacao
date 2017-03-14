using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Autenticacao.Api.Helpers
{
    public static class TaskCompletionSourceFactory
    {
        public static Task<HttpResponseMessage> ToAsyncTask(this HttpResponseMessage response)
        {
            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }
        public static Task<HttpResponseMessage> CreateResponseAsyncTaskOk<T>(this HttpRequestMessage request, T value)
        {
            return request.CreateResponse(HttpStatusCode.OK, value).ToAsyncTask();
        }
        public static Task<HttpResponseMessage> CreateResponseAsyncTaskBadRequest<T>(this HttpRequestMessage request, T value)
        {
            return request.CreateResponse(HttpStatusCode.BadRequest, value).ToAsyncTask();
        }
    }
}
