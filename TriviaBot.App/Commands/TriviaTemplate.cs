using Discord;
using Discord.Commands;
using RestSharp;

using TriviaBot.App.Models;

namespace TriviaBot.App.Commands
{
    public class TriviaTemplate : CommandBase
    {
        private static readonly Random random = new Random();

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

                    var answerChoices = new List<string> { "A", "B", "C", "D" };
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
                        .WithImageUrl(
                            "https://images.squarespace-cdn.com/content/v1/5a328d66a8b2b051a8d2f017/1567530555218-OF3Y7UYVG767NHSMP46D/trivianight.gif")
                        .Build();

                    await ReplyAsync(embed: questionEmbed, components: components);
                    
                    var fiveSecCountEmbed = new EmbedBuilder()
                        .WithColor(Color.DarkRed)
                        .WithImageUrl("https://i.gifer.com/origin/07/07dbafd836794dc3e165ef798aae5b59_w200.gif")
                        .Build();

                    await Task.Delay(TimeSpan.FromSeconds(10));
                    await ReplyAsync(embed: fiveSecCountEmbed);
                    
                    var timeUpEmbed = new EmbedBuilder()
                        .WithColor(Color.Red)
                        .WithTitle("⌛ Time's Up!")
                        .WithDescription($"The correct answer is: **{decodedCorrectAnswer}**")
                        .Build();
                    
                    await Task.Delay(TimeSpan.FromSeconds(5));
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
            string decodedQuestion = System.Net.WebUtility.HtmlDecode(rawHtml);
            string cleanedQuestion = decodedQuestion.Replace("\"", "");

            return cleanedQuestion;
        }

        private List<T> ShuffleAnswers<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }

            return list;
        }
    }
}
