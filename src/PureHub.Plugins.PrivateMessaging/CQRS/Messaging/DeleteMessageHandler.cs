using PureHub.CQRS.Messaging;
using PureHub.CQRS.Repositories;
using PureHub.Plugins.PrivateMessaging.CQRS.Domain;
using PureHub.Plugins.PrivateMessaging.CQRS.Exceptions;
using PureHub.Plugins.PrivateMessaging.Resources;

namespace PureHub.Plugins.PrivateMessaging.CQRS.Messaging
{
    internal sealed class DeleteMessageHandler
        : ICommandHandler<DeleteMessage>
    {
        private readonly IRepository<Message> _repository;

        public DeleteMessageHandler(IRepository<Message> repository)
        {
            _repository = repository;
        }

        public void Handle(DeleteMessage command)
        {
            var message = _repository.GetById(command.AggregateRootId);
            if (message == null)
            {
                throw new MessageException(command.AggregateRootId, command.ExpectedVersion, string.Format(ExceptionResources.MessageNotFound, command.AggregateRootId));
            }

            message.Delete(command.RecipientId);

            _repository.Save(message, command.ExpectedVersion);
        }
    }
}