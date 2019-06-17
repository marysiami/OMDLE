using Microsoft.Extensions.DependencyInjection;
using Omdle.Account.Contracts;
using Omdle.Account.Services;

namespace Omdle.Account
{
    public static class ServiceConfigurator
    {
        public static void RegisterAccountModule(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthValidationService, AuthValidationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
        }
    }
}
