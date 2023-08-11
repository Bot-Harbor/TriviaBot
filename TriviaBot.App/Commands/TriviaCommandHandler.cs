using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using RestSharp;
using TriviaBot.App.Models;

namespace TriviaBot.App.Commands
{
    public class TriviaCommandHandler : CommandBase
    {
        private static readonly Random Random = new Random();
        public static int CorrectAnswerIndex { get; set; }

        protected async Task TriviaCommandAsync(string endpoint, string category, Color embedColor, string img)
        {
            var restClient = new RestClient();
            var request = new RestRequest(endpoint);
            var response = await restClient.ExecuteAsync<TriviaModel>(request);

            if (response.Data != null && response.Data.Results.Count > 0)
            {
                var triviaQuestion = response.Data.Results[0];
                if (triviaQuestion is {Category: var questionCategory} && questionCategory == category)
                {
                    var decodedQuestion = DecodeHtml(triviaQuestion.Question);
                    var decodedCorrectAnswer = DecodeHtml(triviaQuestion.Correct_Answer);
                    var decodedAllAnswers = triviaQuestion.AllAnswers.Select(DecodeHtml).ToList();

                    var shuffledChoices = ShuffleAnswers(decodedAllAnswers);
                    var correctAnswerIndex = shuffledChoices.IndexOf(decodedCorrectAnswer);
                    CorrectAnswerIndex = correctAnswerIndex;
                    var answerChoices = new List<string> {"A", "B", "C", "D"};

                    var questionEmbed = new EmbedBuilder()
                        .WithColor(embedColor)
                        .WithTitle($"🗃️ Category: {triviaQuestion.Category}")
                        .WithDescription($"💪🏻 **Difficulty: **{triviaQuestion.Difficulty.ToUpper()}")
                        .AddField($"❓ Question:", $"```{decodedQuestion}```", inline: false)
                        .WithImageUrl(img)
                        .Build();

                    var answerButtons = new ComponentBuilder();
             
                    for (var i = 0; i < shuffledChoices.Count; i++)
                    {
                        answerButtons.WithButton(new ButtonBuilder()
                            .WithLabel($"{answerChoices[i]}. {shuffledChoices[i]}")
                            .WithCustomId($"choice_{i}")
                            .WithStyle(ButtonStyle.Secondary)
                            .WithDisabled(false));
                    }

                    var buttonComponent = answerButtons.Build();
                    await ReplyAsync(embed: questionEmbed, components: buttonComponent);
                }
                else
                {
                    await ReplyAsync("No trivia questions available.");
                }
            }
        }
        
        public async Task ButtonHandler(SocketMessageComponent component, int correctAnswerIndex)
        {
            var selectedChoiceIndex = int.Parse(component.Data.CustomId.Substring("choice_".Length));

            if (selectedChoiceIndex == correctAnswerIndex)
            {
                var correctResponseEmbed = new EmbedBuilder()
                    .WithColor(Color.DarkGreen)
                    .WithTitle($"️**✔️ Correct, {component.User.Username}!**")
                    .Build();

                await component.RespondAsync(embed: correctResponseEmbed);
            }
            else
            {
                var answerLetters = new[] {"A", "B", "C", "D"};

                var incorrectAnswerEmbed = new EmbedBuilder()
                    .WithColor(Color.DarkRed)
                    .WithTitle(
                        $"**❌ Incorrect, {component.User.Username}!{Environment.NewLine}**")
                    .WithDescription($"```The correct answer is: {answerLetters[correctAnswerIndex]}```")
                    .Build();

                await component.RespondAsync(embed: incorrectAnswerEmbed);
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