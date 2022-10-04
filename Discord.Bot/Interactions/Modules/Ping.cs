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
    public class PingModule : CustomInteractionModuleBase
    {
        public QuestionsService questionsService { get; set; }

        [SlashCommand("test", "test")]
        public async Task test()
        {
            await DeferAsync();
            await questionsService.AskUserToFillQuestionAsync(user: Context.User);
        }


        [ComponentInteraction("ping.test.ready")]
        public async Task ready()
        {
            await DeferAsync();

            var result = await questionsService.TrySendUserQuestionAsync(Context.BotUser);
        }
        [ComponentInteraction("ping.test.snooze")]
        public async Task snooze()
        {
            await DeferAsync();
        }
        [ComponentInteraction("ping.test.skip")]
        public async Task skip()
        {
            await DeferAsync();
        }
    }

}
