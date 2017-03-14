using System.Web.Http;
using Autenticacao.Api.Helpers;
using Autenticacao.AplicacaoServico;
using Autenticacao.Dominio.Repositorios;
using Autenticacao.Dominio.Servicos;
using Autenticacao.Infra.Repositorios;
using Autenticacao.Persistencia.DataContexts;
using Autenticacao.SharedKernel;
using Autenticacao.SharedKernel.Events;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace Autenticacao.Api.StartupApp
{
    public partial class Startup
    {
        private void ConfigureDependencyInjection(HttpConfiguration config, Container container)
        {
            var lifestyle = new WebApiRequestLifestyle();
            container.Register<DataContext, DataContext>(lifestyle);

            container.Register<Autenticacao.Persistencia.Interfaces.IUnitOfWork, Autenticacao.Persistencia.UnitOfWork>(lifestyle);

            container.Register<IUsuarioRepositorio, UsuarioRepositorio>(lifestyle);
            container.Register<IUsuarioAplicacaoServico, UsuarioAplicacaoServico>(lifestyle);



            container.Register<IAppKeyRepositorio, AppKeyRepositorio>(lifestyle);
            container.Register<IAppKeyAplicacaoServico, AppKeyAplicacaoServico>(lifestyle);

            container.Register<IHandler<DomainNotification>, DomainNotificationHandler>(lifestyle);

            config.DependencyResolver = new SimpleInjector.Integration.WebApi.SimpleInjectorWebApiDependencyResolver(container);
            DomainEvent.Container = new DominioEventosContainer(config.DependencyResolver);
        }

    }
}