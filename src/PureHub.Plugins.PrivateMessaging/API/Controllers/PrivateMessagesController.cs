using System;
using System.Net;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using PureHub.CQRS.Messaging;
using PureHub.Plugins.PrivateMessaging.API.Models;
using PureHub.Plugins.PrivateMessaging.CQRS.Messaging;

namespace PureHub.Plugins.PrivateMessaging.API.Controllers
{
    [RoutePrefix("api/plugins/private-messages")]
    public sealed class PrivateMessagesController
        : ApiController
    {
        private readonly ICommandSender _commandSender;

        public PrivateMessagesController(ICommandSender commandSender)
        {
            _commandSender = commandSender;
        }

        [HttpGet, Route(""), Authorize]
        public IHttpActionResult Get(int pageSize, int pageOffset)
        {
            return Ok();
        }

        [HttpGet, Route("{id:guid}"), Authorize]
        public IHttpActionResult Get([FromUri] Guid id)
        {
            return Ok();
        }

        [HttpPost, Route(""), Authorize]
        public IHttpActionResult Post([FromBody] NewMessage model)
        {
            var command = model.SaveOnly
                ? new SaveNewMessage(model.Sender, model.Recipients, model.Title, model.Content)
                : new SendNewMessage(model.Sender, model.Recipients, model.Title, model.Content) as Command;

            _commandSender.Send(command);

            return Created(CreateResourceUrl(command.AggregateRootId), new { id = command.AggregateRootId });
        }

        [HttpPut, Route("{id:guid}"), Authorize]
        public IHttpActionResult Put([FromUri] Guid id, [FromBody] DraftMessage model)
        {
            var command = model.SaveOnly
                ? new SaveDraftMessage(id, model.Version, model.Sender, model.Recipients, model.Title, model.Content)
                : new SendDraftMessage(id, model.Version, model.Sender, model.Recipients, model.Title, model.Content) as Command;

            _commandSender.Send(command);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPut, Route("{id:guid}/read"), Authorize]
        public IHttpActionResult MarkAsRead([FromUri] Guid id, [FromBody] MessageState model)
        {
            var recipientId = new Guid(User.Identity.GetUserId());

            var command = new MarkMessageAsRead(id, model.Version, recipientId);

            _commandSender.Send(command);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPut, Route("{id:guid}/unread"), Authorize]
        public IHttpActionResult MarkAsUnread([FromUri] Guid id, [FromBody] MessageState model)
        {
            var recipientId = new Guid(User.Identity.GetUserId());

            var command = new MarkMessageAsUnread(id, model.Version, recipientId);

            _commandSender.Send(command);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete, Route("{id:guid}"), Authorize]
        public IHttpActionResult Delete([FromUri] Guid id, [FromBody] MessageState model)
        {
            var recipientId = new Guid(User.Identity.GetUserId());

            var command = new DeleteMessage(id, model.Version, recipientId);

            _commandSender.Send(command);

            return StatusCode(HttpStatusCode.NoContent);
        }

        private string CreateResourceUrl(Guid id)
        {
            var resourceUrl = $"{Request.RequestUri.GetLeftPart(UriPartial.Path)}/{id}";

            return resourceUrl;
        }
    }
}