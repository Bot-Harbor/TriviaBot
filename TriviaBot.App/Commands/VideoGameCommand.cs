using Discord;
using Discord.Commands;

namespace TriviaBot.App.Commands;

public class VideoGameCommand : TriviaCommandHandler
{
    [Command("video games")]
    public async Task SportsCommandAsync()
    {
        await TriviaCommandAsync("https://opentdb.com/api.php?amount=1&category=15&type=multiple", "Entertainment: Video Games",
            Color.Purple);
    }
}