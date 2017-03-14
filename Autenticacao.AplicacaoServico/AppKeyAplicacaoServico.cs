using System;
using Autenticacao.Dominio.Entidades;
using Autenticacao.Dominio.Repositorios;
using Autenticacao.Dominio.Servicos;
using Autenticacao.Persistencia.Interfaces;

namespace Autenticacao.AplicacaoServico
{
    public class AppKeyAplicacaoServico : Autenticacao.AplicacaoServico.AplicacaoServico, IAppKeyAplicacaoServico
    {
        private readonly IAppKeyRepositorio _repositorio;
        public AppKeyAplicacaoServico(IAppKeyRepositorio repositorio, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _repositorio = repositorio;
        }

        public AppKey Obter(Guid chave)
        {
            return _repositorio.Obter(chave);
        }
    }
}