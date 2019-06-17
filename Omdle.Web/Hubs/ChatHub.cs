using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Omdle.Account.Contracts;
using Omdle.Chat.Contracts;
using Omdle.Web.ViewModels;

namespace Omdle.Web.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IUserService _userService;
        private readonly IChatService _chatService;

        public ChatHub(
            IUserService userService,
            IChatService chatService
        )
        {
            _userService = userService;
            _chatService = chatService;
        }

        public async Task SendMessage(string message,
            string userId)
        {
            var messageOwner = await _userService.GetUserByIdAsync(userId);

            var chatMessage = await _chatService.SaveMessageAsync(message,
                messageOwner);

            var chatMessageViewModel = new ChatMessageViewModel(chatMessage);

            await Clients.All.SendAsync("ReceiveMessage",
                JsonConvert.SerializeObject(chatMessageViewModel,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    }));
        }
    }
}