using System;
using System.Linq;
using Autenticacao.Dominio.Entidades;
using Autenticacao.Dominio.Repositorios;
using Autenticacao.Persistencia.DataContexts;

namespace Autenticacao.Infra.Repositorios
{
    public class AppKeyRepositorio : IAppKeyRepositorio
    {
        private readonly DataContext _context;
        public AppKeyRepositorio(DataContext context)
        {
            _context = context;
        }

        public AppKey Obter(Guid chave)
        {
            return _context.AppKeys.FirstOrDefault(c => c.Chave == chave);
        }
    }
}