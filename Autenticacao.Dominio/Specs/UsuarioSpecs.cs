using System;
using System.Linq.Expressions;
using Autenticacao.Dominio.Entidades;
using Autenticacao.SharedKernel.Helpers;

namespace Autenticacao.Dominio.Specs
{
    public static class UsuarioSpecs
    {
        public static Expression<Func<Usuario, bool>> AutenticarUsuario(string login, string senha)
        {
            string senhaCriptografada = senha.Encrypt();
            return x => x.Login == login && x.Senha == senhaCriptografada && (x.Status == "A" || x.Status == "T");
        }
    }
}
