using Discord;
using Discord.Commands;

namespace TriviaBot.App.Commands;

public class GeographyCommand : TriviaCommandHandler
{
    [Command("geography")]
    public async Task SportsCommandAsync()
    {
        await TriviaCommandAsync("https://opentdb.com/api.php?amount=1&category=22&type=multiple", "Geography",
            Color.Green,
            "https://media0.giphy.com/media/v1.Y2lkPTc5MGI3NjExMHpyNDZzaDR0azV4N3dva2lrbmZjOGExaDVqeWg0MTRydHhuZHF4ayZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9Zw/9JgeSP0jlRAVBOG9FD/giphy.gif");
    }
}