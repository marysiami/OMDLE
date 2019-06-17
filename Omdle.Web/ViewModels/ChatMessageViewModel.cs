using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Omdle.Data.Models;
using Omdle.Data.Models.Account;

namespace Omdle.Web.ViewModels
{
    public class ChatMessageViewModel
    {
        public string Message { get; set; }
        public string OwnerId { get; set; }
        public DateTime SendTime { get; set; }

        public ChatMessageViewModel(ChatMessage model)
        {
            Message = model.Message;
            OwnerId = model.UserId.ToString();
            SendTime = model.SendTime;
        }
    }
}
