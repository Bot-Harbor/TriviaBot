using Discord;
using Discord.Commands;

namespace TriviaBot.App.Commands;

public class AnimalCommand : TriviaCommandHandler
{
    [Command("animals")]
    public async Task SportsCommandAsync()
    {
        await TriviaCommandAsync("https://opentdb.com/api.php?amount=1&category=27&type=multiple", "Animals",
            Color.Gold);
    }
}