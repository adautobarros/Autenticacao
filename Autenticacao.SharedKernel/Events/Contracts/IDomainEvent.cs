using System;

namespace Autenticacao.SharedKernel.Events.Contracts
{
    public interface IDomainEvent
    {
        DateTime DateOccurred { get; }
    }
}
