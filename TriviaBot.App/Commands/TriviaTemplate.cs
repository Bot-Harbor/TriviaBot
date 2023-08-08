using Discord;
using Discord.Commands;
using RestSharp;
using TriviaBot.App.Models;

namespace TriviaBot.App.Commands
{
    public class TriviaTemplate : CommandBase
    {
        private static readonly Random Random = new Random();

        [Command("video games")]
        public async Task TriviaCommandAsync()
        {
            var restClient = new RestClient();
            var request = new RestRequest("https://opentdb.com/api.php?amount=1&category=15&type=multiple");
            var response = await restClient.ExecuteAsync<TriviaModel>(request);

            if (response.Data != null && response.Data.Results.Count > 0)
            {
                var triviaQuestion = response.Data.Results[0];
                if (triviaQuestion != null && triviaQuestion.Category == "Entertainment: Video Games")
                {
                    var decodedQuestion = DecodeHtml(triviaQuestion.Question);
                    var decodedCorrectAnswer = DecodeHtml(triviaQuestion.Correct_Answer);
                    var decodedAllAnswers = triviaQuestion.AllAnswers.Select(DecodeHtml).ToList();

                    var answerChoices = new List<string> {"A", "B", "C", "D"};
                    var shuffledChoices = ShuffleAnswers(decodedAllAnswers);
                    var buttons = new ComponentBuilder();

                    for (int i = 0; i < shuffledChoices.Count; i++)
                    {
                        buttons.WithButton(new ButtonBuilder().WithLabel($"{answerChoices[i]}. {shuffledChoices[i]}")
                            .WithCustomId($"choice_{i}").WithStyle(ButtonStyle.Secondary));
                    }

                    var components = buttons.Build();

                    var questionEmbed = new EmbedBuilder()
                        .WithColor(Color.Gold)
                        .WithTitle($"Category: {triviaQuestion.Category}")
                        .WithDescription($"**Difficulty: **{triviaQuestion.Difficulty.ToUpper()}")
                        .AddField("Question:", decodedQuestion, inline: false)
                        .WithFooter("You have 15 seconds to answer.")
                        .WithImageUrl(
                            "https://images.squarespace-cdn.com/content/v1/5a328d66a8b2b051a8d2f017/1567530555218-OF3Y7UYVG767NHSMP46D/trivianight.gif")
                        .Build();

                    await ReplyAsync(embed: questionEmbed, components: components);

                    await Task.Delay(TimeSpan.FromSeconds(3));

                    var fiveSecCountEmbed = new EmbedBuilder()
                        .WithTitle("Time Remaining: ")
                        .WithColor(Color.Gold)
                        .Build();

                    var message = await ReplyAsync(embed: fiveSecCountEmbed);

                    for (var i = 5; i >= 0; i--)
                    {
                        fiveSecCountEmbed = new EmbedBuilder()
                            .WithTitle($"Time Remaining: {i}")
                            .WithColor(Color.Gold)
                            .Build();

                        await message.ModifyAsync(properties => properties.Embed = fiveSecCountEmbed);
                        await Task.Delay(TimeSpan.FromSeconds(1));
                    }

                    var timeUpEmbed = new EmbedBuilder()
                        .WithColor(Color.Red)
                        .WithTitle("⌛ Time's Up!")
                        .WithDescription($"The correct answer is: **{Environment.NewLine}{decodedCorrectAnswer}**")
                        .Build();

                    await ReplyAsync(embed: timeUpEmbed);
                }
                else
                {
                    await ReplyAsync("No trivia questions available.");
                }
            }
        }

        private string DecodeHtml(string rawHtml)
        {
            var decodedQuestion = System.Net.WebUtility.HtmlDecode(rawHtml);
            var cleanedQuestion = decodedQuestion.Replace("\"", "");

            return cleanedQuestion;
        }

        private List<T> ShuffleAnswers<T>(List<T> list)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = Random.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }

            return list;
        }
    }
}