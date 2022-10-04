using Discord.Interactions;
using Discord.WebSocket;
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
        public Locale UserLocale { get; }
        public Locale GuildLocale { get; }
        public User BotUser { get; set; }
        public CustomSocketInteractionContext(DiscordSocketClient client, SocketInteraction interaction, Locale guildLocale, Locale userLocale, User botUser) : base(client, interaction)
        {
            UserLocale = userLocale;
            GuildLocale = guildLocale;
            BotUser = botUser;
        }

    }
}
