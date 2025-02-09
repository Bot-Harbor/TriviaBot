using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Triviabot.App.Services;
using Triviabot.App.Slash_Commands.Embeds;
using Triviabot.App.Slash_Commands.Enums;

namespace Triviabot.App.Slash_Commands;

public class QuestionCommand : ApplicationCommandModule
{
    [SlashCommand("question", "Request a question.")]
    public async Task QuestionCommandAsync(InteractionContext context,
        [Option("category", "Choose a category")]
        Category category = default)
    {
        await context.DeferAsync();

        var client = new HttpClient();
        var triviaService = new TriviaService(client);
        var trivia = await triviaService.Get(category.GetHashCode());

        if (trivia == null)
        {
            var noQuestionsEmbed = new NoQuestionsEmbed();
            await context.FollowUpAsync(new DiscordFollowupMessageBuilder().AddEmbed(noQuestionsEmbed.Build()));
            return;
        }

        if (trivia.ResponseCode == 5)
        {
            var rateLimitEmbed = new RateLimitEmbed();
            await context.FollowUpAsync(new DiscordFollowupMessageBuilder().AddEmbed(rateLimitEmbed.Build()));
            return;
        }

        var questionEmbed = new QuestionEmbed();
        var results = trivia.Results.FirstOrDefault();

        var messageInfo = await context.FollowUpAsync(new DiscordFollowupMessageBuilder(questionEmbed.Build(results)));

        if (!Bot.QuestionMessages.ContainsKey(messageInfo.Id))
            Bot.QuestionMessages.Add(messageInfo.Id, new QuestionMessage());

        var questionMessage = Bot.QuestionMessages.GetValueOrDefault(messageInfo.Id);
        questionMessage.CorrectAnswer = results!.CorrectAnswer;
        questionMessage.IncorrectAnswers.AddRange(results.IncorrectAnswers);
    }
}