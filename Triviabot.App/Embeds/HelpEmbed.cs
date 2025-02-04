using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace Triviabot.App.Embeds;

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
            "\ud83c\udfb2  **Game Commands**",
            "\ud83d\udca1  </general:>\n" +
            "\ud83d\udcd6  </history:>\n" +
            "\ud83e\uddea  </science:>\n" +
            "\ud83d\udc2f  </animals:>\n" +
            "\ud83c\udf0f   </geography:>\n" +
            "\u26bd  </sports:>\n" +
            "\ud83d\udd79\ufe0f  </video-games:>\n" +
            "\ud83c\udfb6   </music:>\n",
            true
        );

        helpEmbed.AddField
        (
            "\ud83d\udee0\ufe0f  **Other Commands**",
            "\ud83c\udd98  </help:>\n" +
            "\ud83c\udfd3  </ping:1336092230888325141>\n",
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