using Discord;
using Discord.Commands;

namespace TriviaBot.App.Commands;

public class TriviaMusicCommand : TriviaCommandHandler
{
    [Command("music")]
    public async Task SportsCommandAsync()
    {
        await TriviaCommandAsync("https://opentdb.com/api.php?amount=1&category=12&type=multiple", "Entertainment: Music",
            Color.Magenta);
    }
}