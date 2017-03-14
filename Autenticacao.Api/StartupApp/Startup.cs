using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Autenticacao.Api.StartupApp;
using Autenticacao.Api.Tracing;
using Autenticacao.Dominio.Servicos;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;

[assembly: OwinStartup(typeof(Startup))]

namespace Autenticacao.Api.StartupApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            var container = new Container();
            ConfigureDependencyInjection(config, container);
            Register(config);

            container.BeginExecutionContextScope();
            ConfigureOAuth(app, container.GetInstance<IUsuarioAplicacaoServico>());

            config.Services.Add(typeof(IExceptionLogger), new GlobalExceptionLogger());
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
            config.MessageHandlers.Add(new MessageLoggingHandler());

            //Adicionar chave para validar de qual cliente esta vindo a utilizacao da API
            //config.MessageHandlers.Add(new AppKeyHandle());

            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }
    }
}
