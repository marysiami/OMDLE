using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Omdle.Chat.Models;

namespace Omdle.Web.ViewModels
{
    public class ChatListingViewModel
    {
        public List<ChatMessageViewModel> Messages { get; set; }

        public ChatListingViewModel(ChatListing model)
        {
            Messages = model.Messages.Select(x => new ChatMessageViewModel(x)).ToList();
        }
    }
}
