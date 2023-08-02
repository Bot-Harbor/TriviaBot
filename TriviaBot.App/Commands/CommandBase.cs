using Discord.Commands;

namespace TriviaBot.App.Commands;

public class CommandBase : ModuleBase<SocketCommandContext>
{ 
    protected string Mention => Context.Message.Author.Mention;
}