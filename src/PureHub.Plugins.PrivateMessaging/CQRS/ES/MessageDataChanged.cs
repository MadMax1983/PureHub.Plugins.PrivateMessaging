using System;
using System.Collections.Generic;
using PureHub.CQRS.ES;

namespace PureHub.Plugins.PrivateMessaging.CQRS.ES
{
    internal sealed class MessageDataChanged
        : Event
    {
        public MessageDataChanged(Guid aggregateRootId, IReadOnlyCollection<Guid> recipients, string title, string content)
            : base(aggregateRootId)
        {
            Recipients = recipients;

            Title = title;
            Content = content;
        }

        public IReadOnlyCollection<Guid> Recipients { get; }

        public string Title { get; }

        public string Content { get; }
    }
}