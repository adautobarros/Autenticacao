using Autenticacao.SharedKernel;
using System.Collections.Generic;
using Autenticacao.SharedKernel.Events;

namespace Autenticacao.Api
{
    public class DomainNotificationHandler : IHandler<DomainNotification>
    {
        private List<DomainNotification> _notificacoes;

        public DomainNotificationHandler()
        {
            _notificacoes = new List<DomainNotification>();
        }

        public void Handle(DomainNotification args)
        {
            _notificacoes.Add(args);
        }

        public IEnumerable<DomainNotification> Notificar()
        {
            return GetValue();
        }

        private List<DomainNotification> GetValue()
        {
            return _notificacoes;
        }

        public bool ExisteNotificacoes()
        {
            return GetValue().Count > 0;
        }

        public void Dispose()
        {
            this._notificacoes = new List<DomainNotification>();
        }
    }
}