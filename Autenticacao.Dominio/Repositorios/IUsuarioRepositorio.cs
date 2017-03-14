using Autenticacao.Dominio.Entidades;

namespace Autenticacao.Dominio.Repositorios
{
    public interface IUsuarioRepositorio
    {
        Usuario Autenticar(string login, string senha);
    }
}
