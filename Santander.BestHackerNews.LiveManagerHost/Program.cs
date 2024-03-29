﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Santander.BestHackerNews.LiveManagerHost;
using Santander.BestHackerNews.Persistence;

public class Program()
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((ctx, collection) =>
            {
                collection.AddPersistenceForWriting(ctx.Configuration);
                collection.AddHostedService<LiveManagerHostService>();
            });
    }
}