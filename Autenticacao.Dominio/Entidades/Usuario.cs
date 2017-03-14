using Autenticacao.Dominio.Entidades.Base;
using Autenticacao.Dominio.Scopes;
using Autenticacao.SharedKernel.Helpers;

namespace Autenticacao.Dominio.Entidades
{
    public class Usuario : Entidade<int>
    {
        public Usuario()
        {
        }

        public string Nome { get; set; }
        public string Login { get; private set; }
        public string Senha { get; private set; }
        public string Email { get; private set; }
        public string Status { get; private set; }


        public void Autenticar(string login, string password)
        {
            if (!this.AutenticacaoUsuarioScopeIsValid(login, password.Encrypt()))
                return;
        }
    }
}