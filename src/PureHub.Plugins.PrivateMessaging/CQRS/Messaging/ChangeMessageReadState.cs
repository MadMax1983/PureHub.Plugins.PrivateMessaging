using System;
using PureHub.CQRS.Messaging;

namespace PureHub.Plugins.PrivateMessaging.CQRS.Messaging
{
    internal abstract class ChangeMessageReadState
        : Command
    {
        protected ChangeMessageReadState(Guid messageId, int version, Guid recipientId)
            : base(messageId, version)
        {
            RecipientId = recipientId;
        }

        public Guid RecipientId { get; }
    }
}