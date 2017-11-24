using System;
using System.Collections.Generic;
using PureHub.CQRS.Messaging;

namespace PureHub.Plugins.PrivateMessaging.CQRS.Messaging
{
    internal sealed class SendDraftMessage
        : Command
    {
        public SendDraftMessage(Guid messageId, int expectedVersion, Guid sender, IReadOnlyCollection<Guid> recipients, string title, string content)
            : base(messageId, expectedVersion)
        {
            Sender = sender;
            Recipients = recipients;

            Title = title;
            Content = content;
        }

        public Guid Sender { get; }

        public IReadOnlyCollection<Guid> Recipients { get; }

        public string Title { get; }

        public string Content { get; }
    }
}