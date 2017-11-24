using System;
using PureHub.CQRS.Repositories;
using PureHub.Plugins.PrivateMessaging.CQRS.Domain;

namespace PureHub.Plugins.PrivateMessaging.CQRS.Messaging
{
    internal sealed class MarkMessageAsUnreadHandler
        : ChangeMessageReadStateHandler<MarkMessageAsUnread>
    {
        public MarkMessageAsUnreadHandler(IRepository<Message> repository)
            : base(repository)
        {
        }

        protected override void ChangeState(Message message, Guid recipientId)
        {
            message.MarkAsUnread(recipientId);
        }
    }
}