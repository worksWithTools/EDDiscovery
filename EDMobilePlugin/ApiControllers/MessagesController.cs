using EDMobilePlugin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApiHost.ApiControllers
{
    [Authorize]
    public class MessagesController : ApiController
    {
        private static readonly List<Message> Messages = new List<Message>();

        public List<Message> Get()
        {
            return Messages;
        }

        public async Task Post(Message message)
        {
            Messages.Add(message);

            await this.NotifyAsync(WebHookFilterProvider.MessagePostedEvent, new { Message = message });
            Debug.WriteLine($"MessagePostedEvent Notification send for message from {message.Sender}");
        }

        public async Task Delete(long id)
        {
            var messageToDelete = Messages.FirstOrDefault(m => m.Id == id);

            Messages?.Remove(messageToDelete);

            await this.NotifyAsync(WebHookFilterProvider.MessageRemovedEvent, new { Id = id });
            Debug.WriteLine($"MessageRemovedEvent Notification send for message with Id {id}");
        }
    }
}