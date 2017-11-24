using System;

namespace PureHub.Plugins.PrivateMessaging.CQRS.Domain
{
    internal sealed class Recipient
    {
        public Recipient(Guid id, bool hasRead, bool hasDeletedMessage)
        {
            Id = id;

            HasReadMessage = hasRead;

            HasDeletedMessage = hasDeletedMessage;
        }

        public Guid Id { get; }

        public bool HasReadMessage { get; }

        public bool HasDeletedMessage { get; }
    }
}
