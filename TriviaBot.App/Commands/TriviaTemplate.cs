using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using RestSharp;
using TriviaBot.App.Models;

namespace TriviaBot.App.Commands
{
    public class TriviaTemplate : CommandBase
    {
        private static readonly Random Random = new Random();
        public static int CorrectAnswerIndex { get; private set; }
        
        [Command("video games")]
        public async Task TriviaCommandAsync()
        {
            var restClient = new RestClient();
            var request = new RestRequest("https://opentdb.com/api.php?amount=1&category=15&type=multiple");
            var response = await restClient.ExecuteAsync<TriviaModel>(request);

            if (response.Data != null && response.Data.Results.Count > 0)
            {
                var triviaQuestion = response.Data.Results[0];
                if (triviaQuestion is {Category: "Entertainment: Video Games"})
                {
                    var decodedQuestion = DecodeHtml(triviaQuestion.Question);
                    var decodedCorrectAnswer = DecodeHtml(triviaQuestion.Correct_Answer);
                    var decodedAllAnswers = triviaQuestion.AllAnswers.Select(DecodeHtml).ToList();

                    var shuffledChoices = ShuffleAnswers(decodedAllAnswers);
                    var correctAnswerIndex = shuffledChoices.IndexOf(decodedCorrectAnswer);
                    CorrectAnswerIndex = correctAnswerIndex;
                    var answerChoices = new List<string> {"A", "B", "C", "D"};

                    var questionEmbed = new EmbedBuilder()
                        .WithColor(Color.Gold)
                        .WithTitle($"Category: {triviaQuestion.Category}")
                        .WithDescription($"**Difficulty: **{triviaQuestion.Difficulty.ToUpper()}")
                        .AddField("Question:", decodedQuestion, inline: false)
                        .WithImageUrl(
                            "https://images.squarespace-cdn.com/content/v1/5a328d66a8b2b051a8d2f017/1567530555218-OF3Y7UYVG767NHSMP46D/trivianight.gif")
                        .Build();

                    var answerButtons = new ComponentBuilder();
                    for (var i = 0; i < shuffledChoices.Count; i++)
                    {
                        answerButtons.WithButton(new ButtonBuilder()
                            .WithLabel($"{answerChoices[i]}. {shuffledChoices[i]}")
                            .WithCustomId($"choice_{i}").WithStyle(ButtonStyle.Secondary));
                    }
                    
                    var components = answerButtons.Build();

                    await ReplyAsync(embed: questionEmbed, components: components);
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