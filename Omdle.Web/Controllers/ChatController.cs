using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Omdle.Chat.Contracts;
using Omdle.Web.ViewModels;

namespace Omdle.Web.Controllers
{
    public class ChatController : OmdleBaseController
    {
        private readonly IChatService _chatService;

        public ChatController(
            IChatService chatService
        )
        {
            _chatService = chatService;
        }

        [HttpGet]
        public async Task<JsonResult> GetMessages(int skip = 0, int take = 10)
        {
            var model = await _chatService.GetMessagesAsync(skip, take);

            var result = new ChatListingViewModel(model);

            return Json(result);
        }
    }
}