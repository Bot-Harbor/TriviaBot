using Discord;
using Discord.Commands;

namespace TriviaBot.App.Commands;

public class SportsCommand : TriviaCommandHandler
{
    [Command("sports")]
    public async Task SportsCommandAsync()
    {
        await TriviaCommandAsync("https://opentdb.com/api.php?amount=1&category=21&type=multiple", "Sports",
            Color.Orange);
    }
}