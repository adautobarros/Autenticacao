using Autenticacao.Dominio.Entidades;
using Autenticacao.Persistencia.Interfaces;
using Autenticacao.Dominio.Repositorios;
using Autenticacao.Dominio.Servicos;

namespace Autenticacao.AplicacaoServico
{
    public class UsuarioAplicacaoServico : AplicacaoServico, IUsuarioAplicacaoServico
    {
        private readonly IUsuarioRepositorio _repositorio;
        public UsuarioAplicacaoServico(IUsuarioRepositorio repositorio, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this._repositorio = repositorio;
        }
        public Usuario Autenticar(string login, string senha)
        {
            var usuario = _repositorio.Autenticar(login, senha);

            usuario?.Autenticar(login, senha);
            return usuario;
        }
    }
}
