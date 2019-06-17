using Microsoft.Extensions.DependencyInjection;
using Omdle.Data.Services;
using Omdle.Data.Contracts;

namespace Omdle.Data
{
    public static class ServiceConfigurator
    {
        public static void RegisterDataModule(this IServiceCollection services)
        {
            services.AddScoped<IDataService, DataService>();
        }
    }
}
