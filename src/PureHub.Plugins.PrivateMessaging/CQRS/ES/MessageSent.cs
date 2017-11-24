using System;
using PureHub.CQRS.ES;

namespace PureHub.Plugins.PrivateMessaging.CQRS.ES
{
    internal sealed class MessageSent
        : Event
    {
        public MessageSent(Guid aggregateRootId)
            : base(aggregateRootId)
        {
        }

        public bool IsSent => true;

        public bool IsRead => false;
    }
}