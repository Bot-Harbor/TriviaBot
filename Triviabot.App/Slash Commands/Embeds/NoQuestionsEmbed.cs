using DSharpPlus.Entities;

namespace Triviabot.App.Slash_Commands.Embeds;

public class NoQuestionsEmbed
{
    public DiscordEmbedBuilder Build()
    {
        var embed = new DiscordEmbedBuilder
        {
            Description = "\ud83d\ude14 There are no trivia questions available at this time.",
            Color = DiscordColor.Red
        };

        return embed;
    }
}