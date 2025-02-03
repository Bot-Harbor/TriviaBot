using Discord.Commands;

namespace TriviaBot.App.Old.Commands;

public class PingCommand : CommandBase
{
    [Command("ping")]
    public async Task Ping()
    {
        await ReplyAsync("Pinging to server...");
    }
}