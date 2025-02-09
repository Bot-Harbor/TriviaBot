using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace Triviabot.App.Slash_Commands.Embeds;

public class PingEmbed
{
    public DiscordEmbedBuilder Ping(InteractionContext context)
    {
        var embed = new DiscordEmbedBuilder
        {
            Title = $"Pong 🏓 ``{context.Member.Username}``.",
            ImageUrl =
                "https://media3.giphy.com/media/v1.Y2lkPTc5MGI3NjExeWx4" +
                "cTRoeHptOXpsY2o5bHc2MzhkYW9jZTNiYWt5aGx1cjY0bjlnOCZlcD12M" +
                "V9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9Zw/huJZcPZF0AN4hzoxv9/giphy.gif",
            Color = DiscordColor.White
        };

        return embed;
    }
}