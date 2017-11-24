using PureHub.CQRS.Messaging;
using PureHub.CQRS.Repositories;
using PureHub.Plugins.PrivateMessaging.CQRS.Domain;

namespace PureHub.Plugins.PrivateMessaging.CQRS.Messaging
{
    internal sealed class SendNewMessageHandler
        : ICommandHandler<SendNewMessage>
    {
        private readonly IRepository<Message> _repository;

        public SendNewMessageHandler(IRepository<Message> repository)
        {
            _repository = repository;
        }

        public void Handle(SendNewMessage command)
        {
            var message = new Message(command.AggregateRootId, command.Sender, command.Recipients, command.Title, command.Content);

            message.Send();

            _repository.Save(message, command.ExpectedVersion);
        }
    }
}