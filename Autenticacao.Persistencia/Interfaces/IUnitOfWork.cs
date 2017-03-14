using System;

namespace Autenticacao.Persistencia.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
