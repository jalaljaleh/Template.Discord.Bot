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
    [Group("locale", "locale test")]
    public class LocaleModule : CustomInteractionModuleBase<CustomSocketInteractionContext>
    {
        [SlashCommand("hello", "hello")]
        public async Task hello()
        {
            await DeferAsync();
            await FollowupAsync($"user locale {Context.Interaction.UserLocale}: {Context.UserLocale["hello"]}");
            await FollowupAsync($"guild locale {Context.Interaction.GuildLocale}: {Context.GuildLocale["hello"]}");
        }

    }

}
