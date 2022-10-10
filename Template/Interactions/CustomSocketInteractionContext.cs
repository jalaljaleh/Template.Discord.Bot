using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Data;

namespace Template.Interactions
{
    public class CustomSocketInteractionContext : SocketInteractionContext
    {
        private readonly LocaleService _localeService;
        private readonly UsersService _usersService;
        public CustomSocketInteractionContext(DiscordSocketClient client, SocketInteraction interaction, LocaleService services, UsersService usersService) : base(client, interaction)
        {
            _localeService = services;
            _usersService = usersService;
        }
        public SocketInteraction OverridedInteraction { get; set; }

        public object CustomData { get; set; }

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
            var user = await _usersService.GetOrAddAync(Interaction.User.Id);
            return user;
        }

        private Locale _userLocale;
        public Locale UserLocale
        {
            get
            {
                if (_userLocale is null)
                {
                    _userLocale = GetUserLocale();
                }
                return _userLocale;
            }
        }
        public Locale GetUserLocale()
        {
            return _localeService.GetOrDefault(Interaction.UserLocale);
        }


        private Locale _guildLocale;
        public Locale GuildLocale
        {
            get
            {
                if (_guildLocale is null)
                {
                    _guildLocale = GetGuildLocale();
                }
                return _guildLocale;
            }
        }
        public Locale GetGuildLocale()
        {
            return _localeService.GetOrDefault(Interaction.GuildLocale);
        }



    }
}
