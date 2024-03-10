using Microsoft.Extensions.DependencyInjection;
using Santander.BestHackerNews.Application;
using Santander.BestHackerNews.Persistence;

namespace Santander.BestHackerNews.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            // Add services to the container.

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddPersistence();
            services.AddApplication();
            return services;
        }
    }
}