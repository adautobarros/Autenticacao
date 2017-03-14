using Autenticacao.Dominio.Entidades;

namespace Autenticacao.Dominio.Servicos
{
    public interface IUsuarioAplicacaoServico
    {
        Usuario Autenticar(string login, string senha);
    }
}
