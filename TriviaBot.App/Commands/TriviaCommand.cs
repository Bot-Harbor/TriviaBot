using System.Web;
using Discord.Commands;
using RestSharp;
using TriviaBot.App.Models;

namespace TriviaBot.App.Commands;

public class TriviaCommand : ModuleBase<SocketCommandContext>
{
    [Command("video games")]
    public async Task TriviaCommandAsync()
    {
        var restClient = new RestClient();
        var request = new RestRequest("https://opentdb.com/api.php?amount=1&category=15&type=multiple");
        var response = await restClient.ExecuteAsync<TriviaModel>(request);

        if (response.Data != null && response.Data.Results.Count > 0)
        {
            var triviaQuestion = response.Data.Results[0];
            if (triviaQuestion.Category == "Entertainment: Video Games")
            {
                var decodedQuestion = DecodeHtml(triviaQuestion.Question);
                
                var shuffledChoices = ShuffleAnswers(triviaQuestion.AllAnswers);
                var multipleChoice = MultipleChoice(shuffledChoices);
                
                await ReplyAsync(
                    $"**Category: **{triviaQuestion.Category}{Environment.NewLine}" +
                    $"**Difficulty: **{triviaQuestion.Difficulty?.ToUpper()}{Environment.NewLine}{Environment.NewLine}" +
                    $"**Question: **{Environment.NewLine}{decodedQuestion}{Environment.NewLine}{Environment.NewLine}" +
                    $"**Choices: **{Environment.NewLine}{multipleChoice}");
            }
            else
            {
                await ReplyAsync("No trivia questions available for this category.");
            }
        }
        else
        {
            await ReplyAsync("No trivia questions available.");
        }
    }
    
    private string DecodeHtml(string rawHtml)
    {
        string decodedQuestion = HttpUtility.HtmlDecode(rawHtml);
        string cleanedQuestion = decodedQuestion.Replace("\"", "");

        return cleanedQuestion;
    }
    
    private static List<T> ShuffleAnswers<T>(List<T> list)
    {
        var random = new Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }

        return list;
    }
    
    private string MultipleChoice(List<string> choices)
    {
        char[] letters = { 'A', 'B', 'C', 'D'};

        List<string> multipleChoiceFormat = new List<string>();
        
        for (int i = 0; i < choices.Count; i++)
        {
            if (i < letters.Length)
            {
                multipleChoiceFormat.Add($"**{letters[i]}.** {choices[i]}");
            }
            else
            {
                multipleChoiceFormat.Add($"**{i + 1}.** {choices[i]}");
            }
        }

        return string.Join(Environment.NewLine, multipleChoiceFormat);
    }
}