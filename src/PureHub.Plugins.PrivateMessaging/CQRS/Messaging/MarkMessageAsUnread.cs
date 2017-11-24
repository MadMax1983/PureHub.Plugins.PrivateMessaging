using System;

namespace PureHub.Plugins.PrivateMessaging.CQRS.Messaging
{
    internal sealed class MarkMessageAsUnread
        : ChangeMessageReadState
    {
        public MarkMessageAsUnread(Guid messageId, int expectedVersion, Guid recipientId)
            : base(messageId, expectedVersion, recipientId)
        {
        }
    }
}