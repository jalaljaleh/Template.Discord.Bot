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
    [Group("require-locale", "test")]
    public class RLocaleModule : CustomInteractionModuleBase<CustomSocketInteractionContext>
    {
        [SlashCommand("test", "test ratelimit")]
        [RateLimit(20, 1)]
        [RequireLocale(RequireLocale.LocaleType.UserLocale, "en-US", "tr", "ru")]
        public async Task ratelimit()
        {
            await RespondAsync("yep, i support these locales only.");
        }


    }

}
