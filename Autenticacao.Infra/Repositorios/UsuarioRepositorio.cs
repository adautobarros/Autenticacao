using System.Linq;
using Autenticacao.Dominio.Entidades;
using Autenticacao.Dominio.Repositorios;
using Autenticacao.Dominio.Specs;
using Autenticacao.Persistencia.DataContexts;

namespace Autenticacao.Infra.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly DataContext _context;

        public UsuarioRepositorio(DataContext context)
        {
            _context = context;
        }

        public Usuario Autenticar(string login, string senha)
        {
            return _context.Usuarios
                 .FirstOrDefault(UsuarioSpecs.AutenticarUsuario(login, senha));
        }
    }
}
