using System.Net;
using DSharpPlus;
using DSharpPlus.Entities;
using Triviabot.App.Models;

namespace Triviabot.App.Slash_Commands.Embeds;

public class QuestionEmbed
{
    public DiscordMessageBuilder Build(ResultModel result)
    {
        var decodedCategory = WebUtility.HtmlDecode(result!.Category);

        var embed = new DiscordEmbedBuilder
        {
            Title = $"\ud83d\uddc3\ufe0f Category: {WebUtility.HtmlDecode(result!.Category)}",
            Description =
                $"\ud83d\udcaa\ud83c\udffb **Difficulty:** {WebUtility.HtmlDecode(result!.Difficulty.ToUpper())}",
            Color = GetCategoryStyle(decodedCategory).color,
            Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail
            {
                Url = GetCategoryStyle(decodedCategory).url
            }
        };

        embed.AddField("\u2753 Question", $"```{WebUtility.HtmlDecode(result.Question)}```");

        var choices = new List<string> { WebUtility.HtmlDecode(result.CorrectAnswer) };
        foreach (var incorrectAnswer in result.IncorrectAnswers) choices.Add(WebUtility.HtmlDecode(incorrectAnswer));

        Shuffle(choices);

        var answerLetters = new[] { "A", "B", "C", "D" };
        var buttons = new List<DiscordButtonComponent>();

        foreach (var (answerLetter, choice) in answerLetters.Zip(choices))
            buttons.Add(new DiscordButtonComponent(ButtonStyle.Primary, $"{choice}", $"{answerLetter}. {choice}"));

        var messageBuilder = new DiscordMessageBuilder();
        messageBuilder.AddEmbed(embed);
        messageBuilder.AddComponents(buttons);

        return messageBuilder;
    }

    private void Shuffle<T>(IList<T> list)
    {
        var random = new Random();

        var n = list.Count;
        while (n > 1)
        {
            n--;
            var k = random.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }

    private (DiscordColor color, string url) GetCategoryStyle(string category)
    {
        switch (category)
        {
            case "General Knowledge":
                return (DiscordColor.White,
                    "https://www.meme-arsenal.com/memes/cb17481a7f8ab0c9513baed9b3d83b8a.jpg");
            case "History":
                return (DiscordColor.Blue,
                    "https://i0.wp.com/www.thecleverteacher.com/wp-content/uploads/2021/11/Screen-Shot" +
                    "-2022-01-10-at-11.18.30-AM.png?resize=647%2C489&ssl=1");
            case "Science & Nature":
                return (DiscordColor.DarkRed,
                    "https://i.pinimg.com/736x/ca/84/88/ca84886c1a5520a41e971ed009b19f14.jpg");
            case "Animals":
                return (DiscordColor.Gold, "https://i.pinimg.com/564x/dc/2c/81/dc2c81435916fb359b12ff2251f8d3e2.jpg");
            case "Geography":
                return (DiscordColor.Green, "https://images3.memedroid.com/images/UPLOADED283/64642dcfa7aa2.jpeg");
            case "Sports":
                return (DiscordColor.Orange,
                    "https://ftw.usatoday.com/wp-content/uploads/sites/90/2019/12/screen-shot-" +
                    "2019-12-16-at-2.09.43-pm-e1576736056817.jpg?w=1000&h=600&crop=1");
            case "Entertainment: Video Games":
                return (DiscordColor.Purple,
                    "https://pm1.aminoapps.com/5936/217c9f53d2c5d7e671ae4faa48cb2bb07b17c727_00.jpg");
            case "Entertainment: Music":
                return (DiscordColor.Magenta,
                    "https://i.pinimg.com/736x/c6/61/c0/c661c0bbd4c8f0a910fae9e06aebe708.jpg");
        }

        throw new InvalidOperationException();
    }
}