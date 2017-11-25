using System;
using System.Collections.Generic;

namespace PureHub.Plugins.PrivateMessaging.API.Models
{
    public sealed class NewMessage
    {
        public Guid Sender { get; set; }

        public IReadOnlyCollection<Guid> Recipients { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public bool SaveOnly { get; set; }
    }
}