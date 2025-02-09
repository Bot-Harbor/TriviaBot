using DSharpPlus.Entities;

namespace Triviabot.App.Slash_Commands.Embeds;

public class ErrorEmbed
{
    public DiscordEmbedBuilder Build()
    {
        var embed = new DiscordEmbedBuilder
        {
            Title = "\u26a0\ufe0f Oops, something went wrong.",
            Color = DiscordColor.Red
        };

        return embed;
    }
}