using PureHub.CQRS.Messaging;
using PureHub.CQRS.Repositories;
using PureHub.Plugins.PrivateMessaging.CQRS.Domain;

namespace PureHub.Plugins.PrivateMessaging.CQRS.Messaging
{
    internal sealed class SaveNewMessageHandler
        : ICommandHandler<SaveNewMessage>
    {
        private readonly IRepository<Message> _repository;

        public SaveNewMessageHandler(IRepository<Message> repository)
        {
            _repository = repository;
        }

        public void Handle(SaveNewMessage command)
        {
            var message = new Message(command.AggregateRootId, command.Sender, command.Recipients, command.Title, command.Content);

            _repository.Save(message, command.ExpectedVersion);
        }
    }
}