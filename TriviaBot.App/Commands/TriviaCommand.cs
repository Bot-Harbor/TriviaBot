using Discord.Commands;
using RestSharp;
using TriviaBot.App.Models;

namespace TriviaBot.App.Commands;

public class TriviaCommand : CommandBase
{
    [Command("video games")]
    public async Task HandleCommandAsync()
    {
        var restClient = new RestClient();
        var request = new RestRequest("https://opentdb.com/api.php?amount=1&category=15&type=multiple");
        var response = await restClient.ExecuteAsync<TriviaModel>(request);

        // to test API status
        if (response.Data != null && response.Data.ResponseCode == 0)
        {
            await ReplyAsync(response.Data.ResponseCode.ToString());
        }

        if (response.Data != null && response.Data.Results.Count > 0)
        {
            var triviaQuestion = response.Data.Results[0];
            if (triviaQuestion.Category == "Entertainment: Video Games")
            {
                await ReplyAsync(triviaQuestion.Question);
            }
        }
    }
}