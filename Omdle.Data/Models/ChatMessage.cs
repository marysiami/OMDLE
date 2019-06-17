using System;
using System.Collections.Generic;
using System.Text;
using Omdle.Data.Models.Account;


namespace Omdle.Data.Models
{
    public class ChatMessage
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public DateTime SendTime { get; set; }
        public Guid UserId { get; set; }
        public virtual OmdleUser MessageOwner { get; set; }
    }
}
