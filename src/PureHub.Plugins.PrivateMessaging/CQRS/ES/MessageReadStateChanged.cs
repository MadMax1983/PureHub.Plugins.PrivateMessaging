using System;
using PureHub.CQRS.ES;

namespace PureHub.Plugins.PrivateMessaging.CQRS.ES
{
    internal abstract class MessageReadStateChanged
        : Event
    {
        protected MessageReadStateChanged(Guid aggregateRootId, Guid recipientId, bool isRead)
            : base(aggregateRootId)
        {
            RecipientId = recipientId;

            IsRead = isRead;
        }

        public Guid RecipientId { get; }

        public bool IsRead { get; }
    }
}