using Discord;
using Discord.Commands;

namespace TriviaBot.App.Commands;

public class ScienceCommand : TriviaCommandHandler
{
    [Command("science")]
    public async Task SportsCommandAsync()
    {
        await TriviaCommandAsync("https://opentdb.com/api.php?amount=1&category=17&type=multiple", "Science & Nature",
            Color.DarkRed,
            "https://media4.giphy.com/media/13y4eIkxIc1AsM/giphy.gif?cid=ecf05e475kyrchrslhsopfxp5gi9is6825x33667zfqb5kkh&ep=v1_gifs_search&rid=giphy.gif&ct=g");
    }
}