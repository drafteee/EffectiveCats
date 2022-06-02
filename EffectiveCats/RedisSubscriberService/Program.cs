using RedisSubscriberService;
using StackExchange.Redis;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<IConnectionMultiplexer>(x=> ConnectionMultiplexer.Connect("127.0.0.1:12000"));

        services.AddHostedService<Worker>();

    })
    .Build();

await host.RunAsync();
