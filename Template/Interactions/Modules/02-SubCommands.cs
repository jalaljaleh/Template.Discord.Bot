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
    [Group("parent", "parent commands")]
    public class ParentModule : CustomInteractionModuleBase<CustomSocketInteractionContext>
    {
        [SlashCommand("sub-command", "subcommand")]
        public async Task sub()
        {
            await RespondAsync($"/parent sub-command");
        }



        [Group("child-1", "child 1 commands")]
        public class Child1Module : CustomInteractionModuleBase<CustomSocketInteractionContext>
        {
            [SlashCommand("sub-command", "sub command")]
            public async Task custom_sub()
            {
                await RespondAsync($"/parent child sub-command");
            }

        }


        [Group("child-2", "child 2 commands")]
        public class Child2Module : CustomInteractionModuleBase<CustomSocketInteractionContext>
        {

            [SlashCommand("sub-command", "sub command")]
            public async Task custom_sub()
            {
                await RespondAsync($"/parent child sub-command");
            }

            #region // Nesting of sub-commands is unsupported at this time

            //[Group("child-sub", "sub child commands")]
            //public class SubChildModule : CustomInteractionModuleBase<CustomSocketInteractionContext>
            //{
            //    [SlashCommand("sub-command", "sub command")]
            //    public async Task custom_sub()
            //    {
            //        await CustomRespondAsync($"/parent child child-sub sub-command");
            //    }
            //} 
            #endregion
        }


        [Group("child-3", "child 3 commands")]
        public class Child3Module : CustomInteractionModuleBase<CustomSocketInteractionContext>
        {
            [SlashCommand("sub-command-0", "sub command 0")]
            public async Task custom_sub0()
            {
                await RespondAsync($"/parent child sub-command");
            }

            [SlashCommand("sub-command-1", "sub command 1")]
            public async Task custom_sub1()
            {
                await RespondAsync($"/parent child sub-command");
            }

            [SlashCommand("sub-command-2", "sub command 2")]
            public async Task custom_sub2()
            {
                await RespondAsync($"/parent child sub-command");
            }
        }


    }

}
