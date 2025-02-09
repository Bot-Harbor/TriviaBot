using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Triviabot.App.Slash_Commands.Embeds;

namespace Triviabot.App.Events;

public class AnswerButton
{
    public async Task ClientOnComponentInteractionCreated(DiscordClient client,
        ComponentInteractionCreateEventArgs buttonInteraction)
    {
        var channel = buttonInteraction.Interaction;

        var messageId = buttonInteraction.Message.Id;
        var questionMessage = Bot.QuestionMessages.GetValueOrDefault(messageId);
        var messageEmbed = buttonInteraction.Message.Embeds.FirstOrDefault();

        var answerLetters = new[] {"A", "B", "C", "D"};
        var actionRows = buttonInteraction.Message.Components!.ToList()!;
        var buttons = new List<DiscordButtonComponent>();

        try
        {
            if (buttonInteraction.Interaction.Data.CustomId == questionMessage.CorrectAnswer)
            {
                foreach (var actionRow in actionRows)
                {
                    for (var i = 0; i < actionRow.Components.Count; i++)
                    {
                        var component = actionRow.Components[i];
                        ButtonStyle buttonStyle;

                        if (component.CustomId == questionMessage.CorrectAnswer)
                        {
                            buttonStyle = ButtonStyle.Success;
                        }
                        else
                        {
                            buttonStyle = ButtonStyle.Secondary;
                        }

                        buttons.Add(new DiscordButtonComponent(buttonStyle, component.CustomId,
                            $"{answerLetters[i]}. {component.CustomId}", disabled: true));
                    }
                }
            }
            else
            {
                foreach (var actionRow in actionRows)
                {
                    for (var i = 0; i < actionRow.Components.Count; i++)
                    {
                        var component = actionRow.Components[i];
                        ButtonStyle buttonStyle;

                        if (component.CustomId == buttonInteraction.Interaction.Data.CustomId)
                        {
                            buttonStyle = ButtonStyle.Danger;

                            buttons.Add(new DiscordButtonComponent(buttonStyle, component.CustomId,
                                $"{answerLetters[i]}. {component.CustomId}", disabled: true));
                        }
                        else
                        {
                            if (component.CustomId == questionMessage.CorrectAnswer)
                            {
                                buttonStyle = ButtonStyle.Success;
                            }
                            else
                            {
                                buttonStyle = ButtonStyle.Secondary;
                            }

                            buttons.Add(new DiscordButtonComponent(buttonStyle, component.CustomId,
                                $"{answerLetters[i]}. {component.CustomId}", disabled: true));
                        }
                    }
                }
            }

            await channel.CreateResponseAsync(InteractionResponseType.UpdateMessage,
                new DiscordInteractionResponseBuilder().AddEmbed(messageEmbed!).AddComponents(buttons));
            Bot.QuestionMessages.Remove(messageId);
        }
        catch (Exception)
        {
            var errorEmbed = new ErrorEmbed();
            await channel.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                new DiscordInteractionResponseBuilder().AddEmbed(errorEmbed.Build()));
        }
    }
}