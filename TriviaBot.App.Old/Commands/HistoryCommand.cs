using Discord;
using Discord.Commands;

namespace TriviaBot.App.Old.Commands;

public class HistoryCommand : TriviaCommandHandler
{
    [Command("history")]
    public async Task SportsCommandAsync()
    {
        await TriviaCommandAsync("https://opentdb.com/api.php?amount=1&category=23&type=multiple", "History",
            Color.Blue,
            "https://media3.giphy.com/media/l0HlRincOBSIi5oxG/giphy.gif?cid=ecf05e47i20hhfpqz0l4i1k1xekgr58d358y14cwbm87hpz2&ep=v1_gifs_search&rid=giphy.gif&ct=g");
    }
}