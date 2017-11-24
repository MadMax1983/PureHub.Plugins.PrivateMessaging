using System;
using PureHub.CQRS.Repositories;
using PureHub.Plugins.PrivateMessaging.CQRS.Domain;

namespace PureHub.Plugins.PrivateMessaging.CQRS.Messaging
{
    internal sealed class MarkMessageAsReadHandler
        : ChangeMessageReadStateHandler<MarkMessageAsRead>
    {
        public MarkMessageAsReadHandler(IRepository<Message> repository)
            : base(repository)
        {
        }

        protected override void ChangeState(Message message, Guid recipientId)
        {
            message.MarkAsRead(recipientId);
        }
    }
}