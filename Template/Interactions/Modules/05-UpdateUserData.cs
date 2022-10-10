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
    [Group("user", "user commands")]
    public class UserModule : CustomInteractionModuleBase<CustomSocketInteractionContext>
    {
        public EFContext _db { get; set; }

        [SlashCommand("nickname", "user info")]
        public async Task nickname()
        {
            await RespondAsync($"Your Nickname is: {Context.BotUser.Nickname}");
        }

        [SlashCommand("update-nickname", "user info")]
        public async Task update_nickname(string nickname)
        {
            Context.BotUser.Nickname = nickname;
            _db.Update(Context.BotUser);
            await _db.SaveChangesAsync();

            await RespondAsync($"Your name changed to {nickname}");
        }


    }

}
