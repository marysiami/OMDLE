using Microsoft.Extensions.DependencyInjection;
using Omdle.Chat.Contracts;
using Omdle.Chat.Services;

namespace Omdle.Chat
{
    /// <summary></summary>
    public static class ServiceConfigurator
    {
        /// <summary>Registers the chat module.</summary>
        /// <param name="services">The services.</param>
        public static void RegisterChatModule(this IServiceCollection services)
        {
            services.AddScoped<IChatService, ChatService>();
        }
    }
}
