using System;
using PureHub.CQRS.Messaging;
using PureHub.CQRS.Repositories;
using PureHub.Plugins.PrivateMessaging.CQRS.Domain;
using PureHub.Plugins.PrivateMessaging.CQRS.Exceptions;
using PureHub.Plugins.PrivateMessaging.Resources;

namespace PureHub.Plugins.PrivateMessaging.CQRS.Messaging
{
    internal abstract class ChangeMessageReadStateHandler<TCommand>
        : ICommandHandler<TCommand> where TCommand : ChangeMessageReadState
    {
        private readonly IRepository<Message> _repository;

        protected ChangeMessageReadStateHandler(IRepository<Message> repository)
        {
            _repository = repository;
        }

        public void Handle(TCommand command)
        {
            var message = _repository.GetById(command.AggregateRootId);
            if (message == null)
            {
                throw new MessageException(command.AggregateRootId, command.ExpectedVersion, string.Format(ExceptionResources.MessageNotFound, command.AggregateRootId));
            }

            ChangeState(message, command.RecipientId);

            _repository.Save(message, command.ExpectedVersion);
        }

        protected abstract void ChangeState(Message message, Guid recipientId);
    }
}