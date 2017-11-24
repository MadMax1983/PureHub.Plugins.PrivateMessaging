using PureHub.CQRS.Messaging;
using PureHub.CQRS.Repositories;
using PureHub.Plugins.PrivateMessaging.CQRS.Domain;
using PureHub.Plugins.PrivateMessaging.CQRS.Exceptions;
using PureHub.Plugins.PrivateMessaging.Resources;

namespace PureHub.Plugins.PrivateMessaging.CQRS.Messaging
{
    internal sealed class SaveDraftMessageHandler
        : ICommandHandler<SaveDraftMessage>
    {
        private readonly IRepository<Message> _repository;

        public SaveDraftMessageHandler(IRepository<Message> repository)
        {
            _repository = repository;
        }

        public void Handle(SaveDraftMessage command)
        {
            var message = _repository.GetById(command.AggregateRootId);
            if (message == null)
            {
                throw new MessageException(command.AggregateRootId, command.ExpectedVersion, string.Format(ExceptionResources.MessageNotFound, command.AggregateRootId));
            }

            message.ChangeData(command.Recipients, command.Title, command.Content);

            _repository.Save(message, command.ExpectedVersion);
        }
    }
}