using Discord;
using Discord.WebSocket;
using RestSharp;
using TriviaBot.App.Models;

namespace TriviaBot.App.Commands
{
    public class TriviaCommandHandler : CommandBase
    {
        private static readonly Random Random = new Random();
        public static int CorrectAnswerIndex { get; set; }
        private readonly List<ButtonBuilder> _answerButtonBuilder = new List<ButtonBuilder>();
        public static string DisplayAnswer { get; set; }

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
                    DisplayAnswer = decodedCorrectAnswer;
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
                        .WithFooter("*Answer buttons will be hidden after answering")
                        .WithImageUrl(img)
                        .Build();

                    var answerButtons = new ComponentBuilder();

                    for (var i = 0; i < shuffledChoices.Count; i++)
                    {
                        var buttonBuilder = new ButtonBuilder()
                            .WithLabel($"{answerChoices[i]}. {shuffledChoices[i]}")
                            .WithCustomId($"choice_{i}")
                            .WithStyle(ButtonStyle.Secondary)
                            .WithDisabled(false);

                        _answerButtonBuilder.Add(buttonBuilder);

                        answerButtons.WithButton(buttonBuilder);
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
        
        public async Task ButtonHandler(SocketMessageComponent component, int correctAnswerIndex, string correctAnswer)
        {
            var selectedChoiceIndex = int.Parse(component.Data.CustomId.Substring("choice_".Length));

            var answerLetters = new[] {"A", "B", "C", "D"};

            if (selectedChoiceIndex == correctAnswerIndex)
            {
                var correctResponseEmbed = new EmbedBuilder()
                    .WithColor(Color.DarkGreen)
                    .WithTitle($"️**✔️ Correct, {component.User.Username}!**")
                    .WithDescription($"```{answerLetters[correctAnswerIndex]}: {correctAnswer}```")
                    .Build();

                await component.Message.ReplyAsync(embed: correctResponseEmbed);
            }
            else
            {
                var incorrectAnswerEmbed = new EmbedBuilder()
                    .WithColor(Color.DarkRed)
                    .WithTitle(
                        $"**❌ Incorrect, {component.User.Username}!{Environment.NewLine}**")
                    .WithDescription(
                        $"```The correct answer is {answerLetters[correctAnswerIndex]}: {correctAnswer}```")
                    .Build();

                await component.Message.ReplyAsync(embed: incorrectAnswerEmbed);
            }

            var updatedButtons = new ComponentBuilder();

            await component.Message.ModifyAsync(message =>
            {
                foreach (var originalButtonBuilder in _answerButtonBuilder)
                {
                    var updatedButtonBuilder = new ButtonBuilder()
                        .WithLabel(originalButtonBuilder.Label)
                        .WithCustomId(originalButtonBuilder.CustomId)
                        .WithStyle(ButtonStyle.Secondary)
                        .WithDisabled(true);

                    updatedButtons.WithButton(updatedButtonBuilder);
                }

                message.Components = updatedButtons.Build();
            });
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