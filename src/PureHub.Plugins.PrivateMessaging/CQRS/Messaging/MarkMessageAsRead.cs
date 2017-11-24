using System;

namespace PureHub.Plugins.PrivateMessaging.CQRS.Messaging
{
    internal sealed class MarkMessageAsRead
        : ChangeMessageReadState
    {
        public MarkMessageAsRead(Guid messageId, int expectedVersion, Guid recipientId)
            : base(messageId, expectedVersion, recipientId)
        {
        }
    }
}