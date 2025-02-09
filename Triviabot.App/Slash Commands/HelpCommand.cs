using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Triviabot.App.Slash_Commands.Embeds;

namespace Triviabot.App.Slash_Commands;

public class HelpCommand : ApplicationCommandModule
{
    [SlashCommand("help", "Gives information about the bot & available commands.")]
    public async Task PingCommandAsync(InteractionContext context)
    {
        await context.DeferAsync();

        var helpEmbed = new HelpEmbed();

        await context.FollowUpAsync(new DiscordFollowupMessageBuilder(helpEmbed.Help(context)));
    }
}