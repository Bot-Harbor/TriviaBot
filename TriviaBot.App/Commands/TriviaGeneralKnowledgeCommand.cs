using Discord;
using Discord.Commands;

namespace TriviaBot.App.Commands;

public class TriviaGeneralKnowledgeCommand : TriviaCommandHandler
{
    [Command("general knowledge")]
    public async Task SportsCommandAsync()
    {
        await TriviaCommandAsync("https://opentdb.com/api.php?amount=1&category=9&type=multiple", "General Knowledge",
            Color.LightGrey);
    }
}