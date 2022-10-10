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


namespace Template.Interactions.Modules.PrecondinationModules
{
    [Group("ratelimit", "test")]
    public class RatelimitModule : CustomInteractionModuleBase<CustomSocketInteractionContext>
    {
        [SlashCommand("test", "test ratelimit")]
        [RateLimit(20, 1)]
        public async Task ratelimit()
        {
            await RespondAsync("Hello");
        }

        [SlashCommand("clear", "test ratelimit")]
        public async Task clear()
        {
            await RespondAsync($"{RateLimit.Items.Count} deleted.");
            RateLimit.ExpireCommands();
        }
    }

}
