using System.Reflection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using TriviaBot.App.Commands;
using TriviaBot.App.Constants;

namespace TriviaBot.App
{
    class Configure : Secret
    {
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            _client.Log += Log;

            await RegisterCommandsAsync();

            await _client.LoginAsync(TokenType.Bot, Token);

            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private static Task Log(LogMessage logMessage)
        {
            Console.WriteLine(logMessage);
            return Task.CompletedTask;
        }

        private async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);

            _client.ButtonExecuted += (component) => ButtonHandler(component, TriviaTemplate.CorrectAnswerIndex);
        }

        private async Task ButtonHandler(SocketMessageComponent component, int correctAnswerIndex)
        {
            var selectedChoiceIndex = int.Parse(component.Data.CustomId.Substring("choice_".Length));

            if (selectedChoiceIndex == correctAnswerIndex)
            {
                var correctResponseEmbed = new EmbedBuilder()
                    .WithColor(Color.Green)
                    .WithTitle("✔️ Correct ✔️")
                    .Build();

                await component.RespondAsync(embed: correctResponseEmbed);
            }
            else
            {
                var answerLetters = new[] { "A", "B", "C", "D" };

                var incorrectAnswerEmbed = new EmbedBuilder()
                    .WithColor(Color.Red)
                    .WithTitle("❌ Incorrect ❌")
                    .WithDescription($"The correct answer is: {answerLetters[correctAnswerIndex]}")
                    .Build();

                await component.RespondAsync(embed: incorrectAnswerEmbed);
            }
        }

        private async Task HandleCommandAsync(SocketMessage socketMessage)
        {
            var message = socketMessage as SocketUserMessage;
            var context = new SocketCommandContext(_client, message);
            if (message.Author.IsBot) return;

            int argPos = 0;
            if (message.HasStringPrefix("triviabot ", ref argPos))
            {
                await Execute();
            }

            async Task Execute()
            {
                var result = await _commands.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess)
                    Console.WriteLine(result.ErrorReason);
            }
        }
    }
}