using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace Triviabot.App.Slash_Commands.Embeds;

public class HelpEmbed
{
    public DiscordMessageBuilder Help(InteractionContext context)
    {
        var userIcon = context.User.GetAvatarUrl(ImageFormat.Png);
        var userName = context.Client.CurrentUser.Username;
        var botIcon = context.Client.CurrentUser.GetAvatarUrl(ImageFormat.Png);
        var serverCount = context.Client.Guilds.Count;
        var shardCount = context.Client.ShardCount;
        var ping = context.Client.Ping;
        var dSharpPlusVersion = context.Client.VersionString.Substring(0, 5);

        var helpEmbed = new DiscordEmbedBuilder
        {
            Author = new DiscordEmbedBuilder.EmbedAuthor
            {
                Name = $"{userName}",
                Url = "https://github.com/Bot-Harbor/TriviaBot",
                IconUrl = userIcon
            },
            Title = "\ud83d\udcdd Getting Started",
            Color = DiscordColor.Cyan,
            Description =
                "Let's play trivia! Type one of the commands below to get a question. " +
                $"Trivia Bot is powered by [DSharpPlus {dSharpPlusVersion}]" +
                "(https://github.com/DSharpPlus/DSharpPlus) " +
                "and [Docker](https://www.docker.com/).",

            Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail
            {
                Url = botIcon
            },
            Footer = new DiscordEmbedBuilder.EmbedFooter
            {
                Text = "Bot Info  •  " +
                       "Version: 2.0.0  •  " +
                       $"Total Servers: {serverCount}  •  " +
                       $"Shard: {shardCount}  •  " +
                       $"Ping: {ping}"
            }
        };

        helpEmbed.AddField
        (
            "\ud83e\udd16  **Commands**",
            "\ud83c\udd98  </help:1336095627364536433>\n" +
            "\ud83c\udfd3  </ping:1336092230888325141>\n" +
            "\u2753  </question:1336439119320186963>\n", 
            true
        );
        
        helpEmbed.AddField
        (
            "\ud83d\udd20  **Trivia Categories**",
            "\ud83d\udca1  General Knowledge\n" +
            "\ud83d\udcd6  History\n" +
            "\ud83e\uddea  Science\n" +
            "\ud83d\udc2f  Animals\n" +
            "\ud83c\udf0f   Geography\n" +
            "\u26bd   Sports\n" +
            "\ud83d\udd79\ufe0f  Video Games\n" +
            "\ud83c\udfb6   Music\n",
            true
        );

        var addBotBtn = new DiscordLinkButtonComponent
        (
            "https://discord.com/oauth2/authorize?client_id=1136400167411646605\n",
            "Add To A Server"
        );

        var viewRepoBtn = new DiscordLinkButtonComponent
        (
            "https://opentdb.com/",
            "Contribute Questions"
        );

        var messageBuilder =
            new DiscordMessageBuilder(new DiscordMessageBuilder().AddEmbed(helpEmbed)
                .AddComponents(addBotBtn, viewRepoBtn));

        return messageBuilder;
    }
}