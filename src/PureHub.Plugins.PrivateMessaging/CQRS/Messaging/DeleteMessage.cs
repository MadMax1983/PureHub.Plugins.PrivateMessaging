using System;
using PureHub.CQRS.Messaging;

namespace PureHub.Plugins.PrivateMessaging.CQRS.Messaging
{
    internal sealed class DeleteMessage
        : Command
    {
        public DeleteMessage(Guid messageId, int version, Guid recipientId)
            : base(messageId, version)
        {
            RecipientId = recipientId;
        }

        public Guid RecipientId { get; }
    }
}