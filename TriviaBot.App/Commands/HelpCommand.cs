using Discord;
using Discord.Commands;

namespace TriviaBot.App.Commands;

public class HelpCommand : CommandBase
{
    [Command("help")]
    public async Task HandleCommandAsync()
    {
        var helpEmbed = new EmbedBuilder()
            .WithTitle("📝 Geting Started")
            .WithColor(Color.LighterGrey)
            .WithDescription(
                "Let's play trivia! Type one of the commands below to get a question." +
                "\nTriviaBot powered by [Discord.Net 3.0.0](https://discordnet.dev/) and [Docker](https://www.docker.com/).")
            .AddField($"🎲 Game Commands",
                "💡" + $" `triviabot general`{Environment.NewLine}" +
                "📖" + $" `triviabot history`{Environment.NewLine}" +
                "🧪" + $" `triviabot science`{Environment.NewLine}" +
                "🐯" + $" `triviabot animals`{Environment.NewLine}" +
                "🌏" + $" `triviabot geography`{Environment.NewLine}" +
                "⚽" + $" `triviabot sports`{Environment.NewLine}" +
                "🕹️" + $" `triviabot video games`{Environment.NewLine}" +
                "🎶" + " `triviabot music`",
                inline: true)
            .AddField($"🛠️ Other Commands",
                "🆘" + $" `triviabot help`{Environment.NewLine}" +
                "🏓" + " `triviabot ping`",
                inline: true)
            .Build();

        var buttons = new ComponentBuilder();
        buttons.WithButton(new ButtonBuilder()
                .WithLabel("Add TriviaBot To A Server")
                .WithStyle(ButtonStyle.Link)
                .WithUrl(
                    "https://discord.com/api/oauth2/authorize?client_id=1136400167411646605&permissions=8&scope=bot")
            )
            .WithButton(new ButtonBuilder()
                .WithLabel("Contribute Questions")
                .WithStyle(ButtonStyle.Link)
                .WithUrl("https://opentdb.com/")
            );

        var buttonComponent = buttons.Build();
        await ReplyAsync(embed: helpEmbed, components: buttonComponent);
    }
}