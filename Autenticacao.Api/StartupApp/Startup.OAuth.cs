using System;
using Autenticacao.Api.Seguranca;
using Autenticacao.Dominio.Servicos;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace Autenticacao.Api.StartupApp
{
    public partial class Startup
    {
        public void ConfigureOAuth(IAppBuilder app, IUsuarioAplicacaoServico userService)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/v1/private/login"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(2),
                Provider = new SimpleAuthorizationServerProvider(userService)
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}