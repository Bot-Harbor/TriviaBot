using Discord;
using Discord.Commands;

namespace TriviaBot.App.Commands;

public class GeographyCommand : TriviaCommandHandler
{
    [Command("geography")]
    public async Task SportsCommandAsync()
    {
        await TriviaCommandAsync("https://opentdb.com/api.php?amount=1&category=22&type=multiple", "Geography",
            Color.Green);
    }
}