using Discord;
using Discord.Commands;

namespace TriviaBot.App.Commands;

public class HelpCommand : CommandBase
{
    [Command("help")]
    public async Task HandleCommandAsync()
    {
        var helpEmbed = new EmbedBuilder()
            .WithTitle("Trivia Bot Help")
            .WithColor(Color.LighterGrey)
            .WithDescription("Let's play trivia! Type one of the commands below to get a question.\n\n")
            .AddField("🎲 Game Commands", 
                "`triviabot general knowledge`\n" +
                "`triviabot animals`\n" +
                "`triviabot history`\n" +
                "`triviabot science`\n" +
                "`triviabot sports`\n" +
                "`triviabot video games`\n" + 
                "`triviabot music`\n" + 
                "`triviabot geography`", 
                inline: true)
            .AddField("🛠️ Other Commands",
                "`triviabot help`\n" +
                "`triviabot ping`", 
                inline: true)
            .Build();

        var buttons = new ComponentBuilder();
        buttons.WithButton(new ButtonBuilder()
                .WithLabel("Add TriviaBot To A Server")
                .WithStyle(ButtonStyle.Link) 
                .WithUrl("https://discord.com/api/oauth2/authorize?client_id=1136400167411646605&permissions=8&scope=bot")
            )
            .WithButton(new ButtonBuilder()
                .WithLabel("Source Code")
                .WithStyle(ButtonStyle.Link)
                .WithUrl("https://github.com/KalebGarrett/TriviaBot")
            )
            .WithButton(new ButtonBuilder()
                .WithLabel("Host Instance Of TriviaBot")
                .WithStyle(ButtonStyle.Link) 
                .WithUrl("https://hub.docker.com/r/kalebg08/triviabot")
            );

        var buttonComponent = buttons.Build();
        await ReplyAsync(embed: helpEmbed, components: buttonComponent);
    }
}