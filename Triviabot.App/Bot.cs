using DSharpPlus;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.Hosting;
using Triviabot.App.Events;
using Triviabot.App.Slash_Commands;

namespace Triviabot.App;

public class Bot : BackgroundService
{
    public static readonly Dictionary<ulong, QuestionMessage> QuestionMessages = new();
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
        ButtonEvents();
    }

    private void SlashCommands()
    {
        var slashCommands = _client.UseSlashCommands(new SlashCommandsConfiguration {Services = _serviceProvider});
        slashCommands.RegisterCommands<PingCommand>();
        slashCommands.RegisterCommands<HelpCommand>();
        slashCommands.RegisterCommands<QuestionCommand>();
    }

    private void ButtonEvents()
    {
        var answerButton = new AnswerButton();

        _client.ComponentInteractionCreated += async (sender, args) =>
        {
            await answerButton.ClientOnComponentInteractionCreated(sender, args);
        };
    }
}