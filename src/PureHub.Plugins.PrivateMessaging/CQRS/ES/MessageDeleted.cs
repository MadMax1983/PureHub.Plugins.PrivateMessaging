using System;
using PureHub.CQRS.ES;

namespace PureHub.Plugins.PrivateMessaging.CQRS.ES
{
    internal sealed class MessageDeleted
        : Event
    {
        public MessageDeleted(Guid aggregateRootId, Guid recipientId)
            : base(aggregateRootId)
        {
            RecipientId = recipientId;
        }

        public Guid RecipientId { get; }

        public bool IsDeleted => true;
    }
}