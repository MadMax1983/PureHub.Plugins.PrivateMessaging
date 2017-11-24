using System;
using System.Collections.Generic;
using System.Linq;
using PureHub.CQRS.Domain;
using PureHub.Plugins.PrivateMessaging.CQRS.ES;
using PureHub.Plugins.PrivateMessaging.CQRS.Exceptions;
using PureHub.Plugins.PrivateMessaging.Resources;

namespace PureHub.Plugins.PrivateMessaging.CQRS.Domain
{
    internal sealed class Message
        : AggregateRoot
    {
        private IList<Recipient> _recipients;

        public Message()
        {
        }

        public Message(Guid id, Guid sender, IReadOnlyCollection<Guid> recipients, string title, string content)
            : base(id)
        {
            if (!recipients.Any() && string.IsNullOrWhiteSpace(title) && string.IsNullOrWhiteSpace(content))
            {
                throw new MessageException(id, Version, ExceptionResources.InvalidNewMessageData);
            }

            ApplyChange(new MessageCreated(id, sender, recipients, title, content));
        }

        public Guid Sender { get; private set; }

        public IReadOnlyCollection<Recipient> Recipients => _recipients.ToList();

        public string Title { get; private set; }

        public string Content { get; private set; }

        public DateTime? SendDate { get; private set; }

        public void ChangeData(IReadOnlyCollection<Guid> recipients, string title, string content)
        {
            if (SendDate.HasValue)
            {
                throw new MessageException(ExceptionResources.MessageAlreadySent);
            }

            if (!_recipients.Select(r => r.Id).Except(Recipients.Select(r => r.Id)).Any() && Title == title && Content == content)
            {
                throw new MessageException(ExceptionResources.NoMessageDataToChange);
            }

            ApplyChange(new MessageDataChanged(Id, recipients, title, content));
        }

        public void Send()
        {
            if (_recipients.Any() || string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Content))
            {
                throw new MessageException(ExceptionResources.InvalidSendMessageData);
            }

            ApplyChange(new MessageSent(Id));
        }

        public void MarkAsRead(Guid recipientId)
        {
            ValidateRecipientMessage(recipientId);

            ApplyChange(new MessageMarkedAsRead(Id, recipientId));
        }

        public void MarkAsUnread(Guid recipientId)
        {
            ValidateRecipientMessage(recipientId);

            ApplyChange(new MessageMarkedAsUnread(Id, recipientId));
        }

        public void Delete(Guid recipientId)
        {
            if (_recipients.All(r => r.Id != recipientId))
            {
                throw new MessageException(string.Format(ExceptionResources.RecipientNotFoundForMessage, recipientId));
            }

            ApplyChange(new MessageDeleted(Id, recipientId));
        }

        private void Handle(MessageCreated @event)
        {
            Sender = @event.Sender;

            ChangeMessageData(@event.Recipients, @event.Title, @event.Content);
        }

        private void Handle(MessageDataChanged @event)
        {
            ChangeMessageData(@event.Recipients, @event.Title, @event.Content);
        }

        private void Handle(MessageSent @event)
        {
            SendDate = @event.TimeStampUtc.UtcDateTime;
        }

        private void Handle(MessageMarkedAsRead @event)
        {
            Handle((MessageReadStateChanged)@event);
        }

        private void Handle(MessageMarkedAsUnread @event)
        {
            Handle((MessageReadStateChanged)@event);
        }

        private void Handle(MessageDeleted @event)
        {
            var recipient = _recipients.Single(r => r.Id == @event.RecipientId);

            var index = _recipients.IndexOf(recipient);

            _recipients[index] = new Recipient(recipient.Id, recipient.HasReadMessage, @event.IsDeleted);
        }

        private void Handle(MessageReadStateChanged @event)
        {
            var recipient = _recipients.Single(r => r.Id == @event.RecipientId);

            var index = _recipients.IndexOf(recipient);

            _recipients[index] = new Recipient(recipient.Id, @event.IsRead, recipient.HasDeletedMessage);
        }

        private void ChangeMessageData(IEnumerable<Guid> recipients, string title, string content)
        {
            _recipients = recipients.Select(r => new Recipient(r, false, false)).ToList();

            Title = title;
            Content = content;
        }

        private void ValidateRecipientMessage(Guid recipientId)
        {
            if (_recipients.All(r => r.Id != recipientId))
            {
                throw new MessageException(string.Format(ExceptionResources.RecipientNotFoundForMessage, recipientId));
            }

            if (_recipients.Single(r => r.Id == recipientId).HasDeletedMessage)
            {
                throw new MessageException(string.Format(ExceptionResources.RecipientDeletedMessage, recipientId));
            }
        }
    }
}