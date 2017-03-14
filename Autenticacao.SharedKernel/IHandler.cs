using System;
using System.Collections.Generic;
using Autenticacao.SharedKernel.Events.Contracts;

namespace Autenticacao.SharedKernel
{
    public interface IHandler<T> : IDisposable where T : IDomainEvent
    {
        void Handle(T args);
        IEnumerable<T> Notificar();
        bool ExisteNotificacoes();
    }
}
