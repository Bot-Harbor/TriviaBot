using DSharpPlus;
using Microsoft.Extensions.Hosting;

namespace Triviabot.App;

public class Bot : BackgroundService
{
    private readonly DiscordClient _client;
    private readonly IServiceProvider _serviceProvider;
    
    public Bot(DiscordClient client, IServiceProvider serviceProvider)
    {
        _client = client;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _client.ConnectAsync();
    }
}