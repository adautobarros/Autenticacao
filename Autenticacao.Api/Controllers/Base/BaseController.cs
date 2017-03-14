using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Autenticacao.SharedKernel;
using Autenticacao.SharedKernel.Events;
using log4net;

namespace Autenticacao.Api.Controllers
{
    public abstract class BaseController : UsuarioHelperApiController
    {
        public IHandler<DomainNotification> Notifications;
        public HttpResponseMessage Response;
        protected static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected BaseController()
        {
            this.Notifications = DomainEvent.Container.GetService<IHandler<DomainNotification>>();
            this.Response = new HttpResponseMessage();
        }

        //protected abstract HttpRequestMessage HttpRequestMessageInterno { get; }

        private Task<HttpResponseMessage> CreateResponse(HttpStatusCode code, object result = null)
        {
            if (Notifications.ExisteNotificacoes())
                Response = Request.CreateResponse(HttpStatusCode.BadRequest, new { errors = Notifications.Notificar() });
            else
                Response = result == null ? Request.CreateResponse(code) : Request.CreateResponse(code, result);

            return Task.FromResult<HttpResponseMessage>(Response);
        }

        public Task<HttpResponseMessage> InternalServerErrorResponse(object result = null)
        {
            return CreateResponse(HttpStatusCode.InternalServerError, result);
        }
        public Task<HttpResponseMessage> BadRequestResponse(object result = null)
        {
            return CreateResponse(HttpStatusCode.BadRequest, result);
        }
        /// <summary>
        /// HTTP STATUS CODE = 200
        /// NORMALMENTE UTILIZADO PARA ATUALIZAÇÃO DE DADOS
        /// OU QUANDO QUEREMOS DEVOLVER ALGUMA RESPOSTA PARA OUTRAS OPERAÇÕES EX. METODO DELETE
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public Task<HttpResponseMessage> OkResponse(object result = null)
        {
            return CreateResponse(HttpStatusCode.OK, result);
        }
        /// <summary>
        /// HTTP STATUS CODE = 201
        /// UTILIZADO PARA INCLUSÃO DE DADOS 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public Task<HttpResponseMessage> CreatedResponse(object result = null)
        {
            return CreateResponse(HttpStatusCode.Created, result);
        }

        public Task<HttpResponseMessage> NotFoundResponse(object result = null)
        {
            return CreateResponse(HttpStatusCode.NotFound, result);
        }
        /// <summary>
        /// HTTP STATUS CODE = 204
        /// NORMALMENTE UTILIZADO PARA EXCLUSÃO DE DADOS
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> NoContentResponse()
        {
            return await CreateResponse(HttpStatusCode.NoContent);
        }

        public string Location => Request.RequestUri.AbsolutePath;

        protected override void Dispose(bool disposing)
        {
            Notifications.Dispose();
        }
    }
}
