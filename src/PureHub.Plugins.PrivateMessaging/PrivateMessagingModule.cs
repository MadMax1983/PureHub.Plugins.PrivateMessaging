using Ninject.Modules;
using Ninject.Web.Common;
using PureHub.CQRS.Messaging;
using PureHub.Plugins.PrivateMessaging.CQRS.Messaging;

namespace PureHub.Plugins.PrivateMessaging
{
    public sealed class PrivateMessagingModule
        : NinjectModule
    {
        public override void Load()
        {
            Bind<ICommandHandler<SaveNewMessage>>().To<SaveNewMessageHandler>().InRequestScope();
            Bind<ICommandHandler<SaveDraftMessage>>().To<SaveDraftMessageHandler>().InRequestScope();

            Bind<ICommandHandler<SendNewMessage>>().To<SendNewMessageHandler>().InRequestScope();
            Bind<ICommandHandler<SendDraftMessage>>().To<SendDraftMessageHandler>().InRequestScope();

            Bind<ICommandHandler<MarkMessageAsRead>>().To<MarkMessageAsReadHandler>().InRequestScope();
            Bind<ICommandHandler<MarkMessageAsUnread>>().To<MarkMessageAsUnreadHandler>().InRequestScope();

            Bind<ICommandHandler<DeleteMessage>>().To<DeleteMessageHandler>().InRequestScope();
        }
    }
}