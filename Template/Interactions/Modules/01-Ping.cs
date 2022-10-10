using Discord.Interactions;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Template.Data;


namespace Template.Interactions.Modules
{
    public class PingModule : CustomInteractionModuleBase<CustomSocketInteractionContext>
    {
        [SlashCommand("ping", "ping")]
        public async Task ping()
        {
            await RespondAsync($"Pong !, Current Bot Latency: {Context.Client.Latency}");
        }

      
    }

}
