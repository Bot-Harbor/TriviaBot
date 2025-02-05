using DSharpPlus.SlashCommands;

namespace Triviabot.App.Slash_Commands.Enums;

public enum Category
{
    [ChoiceName("General Knowledge")] GeneralKnowledge = 9,
    [ChoiceName("History")] History = 23,
    [ChoiceName("Science")] Science = 17,
    [ChoiceName("Animals")] Animals = 27,
    [ChoiceName("Geography")] Geography = 22,
    [ChoiceName("Sports")] Sports = 21,
    [ChoiceName("Video Games")] VideoGames = 15,
    [ChoiceName("Music")] Music = 12,
}