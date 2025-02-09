using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Triviabot.App.Slash_Commands.Embeds;

namespace Triviabot.App.Slash_Commands;

public class PingCommand : ApplicationCommandModule
{
    [SlashCommand("ping", "Will pong back to the server.")]
    public async Task PingCommandAsync(InteractionContext context)
    {
        await context.DeferAsync();

        var pingEmbed = new PingEmbed();

        await context.FollowUpAsync(new DiscordFollowupMessageBuilder()
            .AddEmbed(pingEmbed.Ping(context)));
    }
}