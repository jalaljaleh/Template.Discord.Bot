using Discord;
using Discord.Commands;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Data;

namespace Template.Commands
{
    public class CustomCommandContext : CommandContext
    {
        private readonly UsersService _usersService;
        private readonly GuildService _guildservice;

        public CustomCommandContext(IDiscordClient client, IUserMessage msg, UsersService usersService, GuildService guildservice) : base(client, msg)
        {
            _usersService = usersService;
            _guildservice = guildservice;
        }

        public object CustomData { get; set; }

        private Guild _botGuild;
        public Guild BotGuild
        {
            get
            {
                if (_botGuild is null)
                {
                    _botGuild = GetBotGuildAsync().Result;
                }
                return _botGuild;
            }
        }
        public async Task<Guild> GetBotGuildAsync()
        {
            if (Guild is null) 
                return null;

            var guild = await _guildservice.GetOrAddAync(Guild.Id);
            return guild;
        }

        private User _botUser;
        public User BotUser
        {
            get
            {
                if (_botUser is null)
                {
                    _botUser = GetBotUserAsync().Result;
                }
                return _botUser;
            }
        }
        public async Task<User> GetBotUserAsync()
        {
            var user = await _usersService.GetOrAddAync(User.Id);
            return user;
        }
    }
}
