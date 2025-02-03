using Discord;
using Discord.Commands;

namespace TriviaBot.App.Old.Commands;

public class SportsCommand : TriviaCommandHandler
{
    [Command("sports")]
    public async Task SportsCommandAsync()
    {
        await TriviaCommandAsync("https://opentdb.com/api.php?amount=1&category=21&type=multiple", "Sports",
            Color.Orange,
            "https://media0.giphy.com/media/fGq2nGGSxMEkU/giphy.gif?cid=ecf05e47q2t555mmzraqnlmfxzcvafufsoxse9limzcmzqc6&ep=v1_gifs_search&rid=giphy.gif&ct=g");
    }
}