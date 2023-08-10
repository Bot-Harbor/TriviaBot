using Discord;
using Discord.Commands;

namespace TriviaBot.App.Commands;

public class ScienceCommand : TriviaCommandHandler
{
    [Command("science")]
    public async Task SportsCommandAsync()
    {
        await TriviaCommandAsync("https://opentdb.com/api.php?amount=1&category=17&type=multiple", "Science & Nature",
            Color.DarkRed);
    }
}