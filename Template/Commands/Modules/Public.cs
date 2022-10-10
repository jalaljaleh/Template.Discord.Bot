using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Commands.Modules
{
    public class Public : ModuleBase<CommandContext>
    {
        [Command("ping")]
        public async Task ping()
        {
            await ReplyAsync("Pong !");
        }
    }
}
