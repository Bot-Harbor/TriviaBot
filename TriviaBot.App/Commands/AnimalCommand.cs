using Discord;
using Discord.Commands;

namespace TriviaBot.App.Commands;

public class AnimalCommand : TriviaCommandHandler
{
    [Command("animals")]
    public async Task SportsCommandAsync()
    {
        await TriviaCommandAsync("https://opentdb.com/api.php?amount=1&category=27&type=multiple", "Animals",
            Color.Gold,
            "https://media0.giphy.com/media/otnqsqqzmsw7K/giphy.gif?cid=ecf05e472a25b6zjrqftjdfunqrmkr9g160vyl4bfh7fkxkw&ep=v1_gifs_search&rid=giphy.gif&ct=g");
    }
}