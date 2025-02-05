using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Triviabot.App.Embeds;
using Triviabot.App.Services;
using Triviabot.App.Slash_Commands.Enums;

namespace Triviabot.App.Slash_Commands;

public class QuestionCommand : ApplicationCommandModule
{
    [SlashCommand("question", "Generates a question.")]
    public async Task QuestionCommandAsync(InteractionContext context,
        [Option("category", "Choose a category")]
        Category category)
    {
        await context.DeferAsync();

        var client = new HttpClient();
        var triviaService = new TriviaService(client);
        var trivia = await triviaService.Get(category.GetHashCode());
        var results = trivia.Results;

        if (results == null || results.Count == 0)
        {
            var noQuestionsEmbed = new NoQuestionsEmbed();
            await context.FollowUpAsync(new DiscordFollowupMessageBuilder().AddEmbed(noQuestionsEmbed.Build()));
        }

        var questionEmbed = new QuestionEmbed();
        await context.FollowUpAsync(new DiscordFollowupMessageBuilder(questionEmbed.Build(results)));
    }
}