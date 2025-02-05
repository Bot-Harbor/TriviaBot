using System.Drawing;
using DSharpPlus.Entities;

namespace Triviabot.App.Embeds;

public class RateLimitEmbed
{
    public DiscordEmbedBuilder Build()
    {
        var embed = new DiscordEmbedBuilder()
        {
            Title = "\u270b Rate limit has been reached! You can only request a question every 5 seconds.",
            Color = DiscordColor.Yellow
        };

        return embed;
    }
}