using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Omdle.Chat.Models;
using Omdle.Data.Models;
using Omdle.Data.Models.Account;

namespace Omdle.Chat.Contracts
{
    /// <summary></summary>
    public interface IChatService
    {
        Task<ChatListing> GetMessagesAsync(int skip = 0, int take = 10);
        Task<ChatMessage> SaveMessageAsync(string message, OmdleUser messageOwner);
    }
}
