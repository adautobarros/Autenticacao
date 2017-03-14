using System;
using Autenticacao.SharedKernel.Events.Contracts;

namespace Autenticacao.SharedKernel.Events
{
    public class DomainNotification : IDomainEvent
    {
        public string Key { get; private set; }
        public string Value { get; private set; }
        public string Extra { get; private set; }
        public DateTime DateOccurred { get; private set; }

        public DomainNotification(string key, string value, string extra = null)
        {
            this.Key = key;
            this.Value = value;
            Extra = extra;
            this.DateOccurred = DateTime.Now;
        }
        public void AddInformacaoExtra(string extra)
        {
            Extra = extra;
        }
    }
}
