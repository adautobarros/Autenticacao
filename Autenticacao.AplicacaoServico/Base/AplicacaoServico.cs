using Autenticacao.Persistencia.Interfaces;
using Autenticacao.SharedKernel;
using Autenticacao.SharedKernel.Events;

namespace Autenticacao.AplicacaoServico
{
    public class AplicacaoServico
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHandler<DomainNotification> _notifications;

        public AplicacaoServico(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._notifications = DomainEvent.Container.GetService<IHandler<DomainNotification>>();
        }

        public bool ExisteNotificacoes()
        {
            return _notifications.ExisteNotificacoes();
        }
        public bool Commit()
        {
            if (_notifications.ExisteNotificacoes())
                return false;

            _unitOfWork.Commit();
            return true;
        }
    }
}
