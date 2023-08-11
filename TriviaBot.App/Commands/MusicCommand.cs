using Discord;
using Discord.Commands;

namespace TriviaBot.App.Commands;

public class MusicCommand : TriviaCommandHandler
{
    [Command("music")]
    public async Task SportsCommandAsync()
    {
        await TriviaCommandAsync("https://opentdb.com/api.php?amount=1&category=12&type=multiple",
            "Entertainment: Music",
            Color.Magenta,
            "https://media3.giphy.com/media/GO5sx8QLOmobbhPVIh/giphy.gif?cid=ecf05e470jhaob0rro8e74vbwa4v1qz2sj74mkjmzt7wsuww&ep=v1_gifs_search&rid=giphy.gif&ct=g");
    }
}