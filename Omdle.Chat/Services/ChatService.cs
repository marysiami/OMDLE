using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Omdle.Chat.Contracts;
using Omdle.Chat.Models;
using Omdle.Data.Contracts;
using Omdle.Data.Models;
using Omdle.Data.Models.Account;

namespace Omdle.Chat.Services
{
    /// <summary></summary>
    /// <seealso cref="Omdle.Chat.Contracts.IChatService" />
    public class ChatService : IChatService
    {
        private readonly IDataService _dataService;

        /// <summary>Initializes a new instance of the <see cref="ChatService"/> class.</summary>
        /// <param name="dataService">The data service.</param>
        public ChatService(IDataService dataService)
        {
            _dataService = dataService;
        }

        /// <summary>Gets the messages asynchronous.</summary>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <returns></returns>
        public async Task<ChatListing> GetMessagesAsync(int skip = 0, int take = 10)
        {
            var query = _dataService.GetSet<ChatMessage>();

            var messages =
                await query
                    .OrderByDescending(m => m.SendTime)
                    .Skip(skip * take)
                    .Take(take)
                    .OrderBy(m => m.SendTime)
                    .ToListAsync();

            var model = new ChatListing
            {
                Messages = messages
            };

            return model;
        }

        /// <summary>Saves the message asynchronous.</summary>
        /// <param name="message">The message.</param>
        /// <param name="messageOwner">The message owner.</param>
        /// <returns></returns>
        public async Task<ChatMessage> SaveMessageAsync(string message, OmdleUser messageOwner)
        {
            var chatMessage = new ChatMessage
            {
                Message = message,
                MessageOwner = messageOwner,
                UserId = messageOwner.Id,
                SendTime = DateTime.Now
            };

            await _dataService.GetSet<ChatMessage>().AddAsync(chatMessage);
            await _dataService.SaveDbAsync();

            return chatMessage;
        }
    }
}
