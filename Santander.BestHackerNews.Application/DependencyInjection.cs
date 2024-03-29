﻿using Microsoft.Extensions.DependencyInjection;

namespace Santander.BestHackerNews.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationForReading(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
            return services;
        }
    }
}