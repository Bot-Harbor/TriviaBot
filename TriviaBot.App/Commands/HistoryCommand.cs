using Discord;
using Discord.Commands;

namespace TriviaBot.App.Commands;

public class HistoryCommand : TriviaCommandHandler
{
    [Command("history")]
    public async Task SportsCommandAsync()
    {
        await TriviaCommandAsync("https://opentdb.com/api.php?amount=1&category=23&type=multiple", "History",
            Color.Blue);
    }
}