using DSharpPlus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Triviabot.App;
using Triviabot.App.Secrets;

var builder = new HostApplicationBuilder();
builder.Services.AddHostedService<Bot>();
builder.Services.AddSingleton<DiscordClient>();

builder.Services.AddSingleton(new DiscordConfiguration
{
    TokenType = TokenType.Bot,
    Token = Discord.Token,
    Intents = DiscordIntents.All,
    MinimumLogLevel = LogLevel.Information,
    AutoReconnect = true,
});

builder.Services.AddLogging(x => x.AddConsole().SetMinimumLevel(LogLevel.Information));

builder.Build().Run();