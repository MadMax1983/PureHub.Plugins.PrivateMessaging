using System;

namespace PureHub.Plugins.PrivateMessaging.CQRS.ES
{
    internal sealed class MessageMarkedAsRead
        : MessageReadStateChanged
    {
        private const bool IS_MESSAGE_READ = true;

        public MessageMarkedAsRead(Guid aggregateRootId, Guid recipientId)
            : base(aggregateRootId, recipientId, IS_MESSAGE_READ)
        {
        }
    }
}