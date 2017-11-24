using System;

namespace PureHub.Plugins.PrivateMessaging.CQRS.ES
{
    internal sealed class MessageMarkedAsUnread
        : MessageReadStateChanged
    {
        private const bool IS_MESSAGE_READ = false;

        public MessageMarkedAsUnread(Guid aggregateRootId, Guid recipientId)
            : base(aggregateRootId, recipientId, IS_MESSAGE_READ)
        {
        }
    }
}