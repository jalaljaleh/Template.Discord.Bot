using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using Template.Data;

namespace Template.Commands
{
    public class CommandsHandler
    {
        private readonly IServiceProvider _services;
        private readonly CommandService _commandService;
        private readonly DiscordSocketClient _discord;
        private readonly GuildService _guildService;
        private readonly UsersService _usersService;
        private readonly BotConfiguration _botConfiguration;
        public CommandsHandler(IServiceProvider services)
        {
            _services = services;
            _commandService = services.GetRequiredService<CommandService>();
            _discord = services.GetRequiredService<DiscordSocketClient>();
            _guildService = services.GetRequiredService<GuildService>();
            _botConfiguration = services.GetRequiredService<BotConfiguration>();
            _usersService = services.GetRequiredService<UsersService>();

            _commandService.CommandExecuted += _commandService_CommandExecuted;
        }

        public async Task InitializeAsync()
        {
            await _commandService.AddModulesAsync(typeof(CommandsHandler).Assembly, _services);
        }
        public async Task<IResult> HandleCommand(SocketUserMessage message)
        {
            var context = new CustomCommandContext(_discord, message, _usersService, _guildService);

            var argPos = 0;
            if (!message.HasStringPrefix(context.BotGuild.Prefix ?? _botConfiguration.Prefix, ref argPos))
                return null;

            return await _commandService.ExecuteAsync(context, argPos, _services);
        }
        private async Task _commandService_CommandExecuted(Discord.Optional<CommandInfo> info, ICommandContext context, IResult result)
        {
            if (result.IsSuccess) return;

            //if command not found
            if (!info.IsSpecified) return;

            await context.Channel.SendMessageAsync(result.ErrorReason);
        }
    }
}
