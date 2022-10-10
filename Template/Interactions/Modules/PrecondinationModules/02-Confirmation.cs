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
    public class ConfirmationModule : CustomInteractionModuleBase<CustomSocketInteractionContext>
    {
        [SlashCommand("confirm", "test confirm")]
        [RequireConfirmation]
        public async Task confirm()
        {
            // You should use this overrided socket interaction instead of Context.Interaction
            await Context.OverridedInteraction.RespondAsync($"Pong !, Current Bot Latency: {Context.Client.Latency}");
        }


    }

}
