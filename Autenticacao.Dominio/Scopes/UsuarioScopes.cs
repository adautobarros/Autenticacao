using Autenticacao.Dominio.Entidades;
using Autenticacao.SharedKernel.Validation;

namespace Autenticacao.Dominio.Scopes
{
    public static class UsuarioScopes
    {
        public static bool AutenticacaoUsuarioScopeIsValid(this Usuario usuario, string login, string senhaEncriptada)
        {
            return AssertionConcern.IsSatisfiedBy
            (
                AssertionConcern.AssertNotEmpty(login, "O Login é obrigatório"),
                AssertionConcern.AssertNotEmpty(senhaEncriptada, "A senha é obrigatória"),
                AssertionConcern.AssertAreEquals(usuario.Login.ToString(), login, "Login ou senha inválidos"),
                AssertionConcern.AssertAreEquals(usuario.Senha.ToUpper(), senhaEncriptada, "Usuário ou senha inválidos")
            );
        }
      
    }
}
