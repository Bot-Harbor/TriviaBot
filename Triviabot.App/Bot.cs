using DSharpPlus;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.Hosting;
using Triviabot.App.Slash_Commands;

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
        SlashCommands();
    }

    private void SlashCommands()
    {
        var slashCommands = _client.UseSlashCommands(new SlashCommandsConfiguration {Services = _serviceProvider});
        slashCommands.RegisterCommands<PingCommand>();
        slashCommands.RegisterCommands<HelpCommand>();
        slashCommands.RegisterCommands<QuestionCommand>();
    }
}