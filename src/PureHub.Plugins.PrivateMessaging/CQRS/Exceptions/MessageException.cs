using System;
using System.Runtime.Serialization;
using PureHub.CQRS.Exceptions;

namespace PureHub.Plugins.PrivateMessaging.CQRS.Exceptions
{
    [Serializable]
    public class MessageException
        : AggregateRootException
    {
        public MessageException()
        {
        }

        public MessageException(string message)
            : base(message)
        {
        }

        public MessageException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public MessageException(Guid aggregateRootId, int aggregateRootVersion)
            : base(aggregateRootId, aggregateRootVersion)
        {
        }

        public MessageException(Guid aggregateRootId, int aggregateRootVersion, string message)
            : base(aggregateRootId, aggregateRootVersion, message)
        {
        }

        public MessageException(Guid aggregateRootId, int aggregateRootVersion, string message, Exception innerException)
            : base(aggregateRootId, aggregateRootVersion, message, innerException)
        {
        }

        protected MessageException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}