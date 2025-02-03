using Discord;
using Discord.Commands;

namespace TriviaBot.App.Old.Commands;

public class GeneralKnowledgeCommand : TriviaCommandHandler
{
    [Command("general")]
    public async Task SportsCommandAsync()
    {
        await TriviaCommandAsync("https://opentdb.com/api.php?amount=1&category=9&type=multiple", "General Knowledge",
            Color.LightGrey,
            "https://media2.giphy.com/media/yumf5Ka76BoW8xGxjU/giphy.gif?cid=ecf05e47ixi05aau3dd0yb4u9kkkfcdzcva0jqb9r6zsnfis&ep=v1_gifs_search&rid=giphy.gif&ct=g");
    }
}