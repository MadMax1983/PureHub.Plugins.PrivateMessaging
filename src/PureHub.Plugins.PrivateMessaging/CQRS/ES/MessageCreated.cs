using System;
using System.Collections.Generic;
using PureHub.CQRS.ES;

namespace PureHub.Plugins.PrivateMessaging.CQRS.ES
{
    internal sealed class MessageCreated
        : Event
    {
        public MessageCreated(Guid aggregateRootId, Guid sender, IReadOnlyCollection<Guid> recipients, string title, string content)
            : base(aggregateRootId)
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