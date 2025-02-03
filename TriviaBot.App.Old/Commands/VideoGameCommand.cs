using Discord;
using Discord.Commands;

namespace TriviaBot.App.Old.Commands;

public class VideoGameCommand : TriviaCommandHandler
{
    [Command("video games")]
    public async Task SportsCommandAsync()
    {
        await TriviaCommandAsync("https://opentdb.com/api.php?amount=1&category=15&type=multiple",
            "Entertainment: Video Games",
            Color.Purple,
            "https://media4.giphy.com/media/12EkClXW5dt9PW/giphy.gif?cid=ecf05e47fzx258986ajpjceu2ahhuwuome2fv3tazuvggbnt&ep=v1_gifs_related&rid=giphy.gif&ct=g");
    }
}