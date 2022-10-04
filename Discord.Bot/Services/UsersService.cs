using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Data;

namespace Template
{
    public class UsersService
    {
        private readonly EFContext _db;
        private readonly DiscordSocketClient _discord;
        private readonly IServiceProvider _services;
        public UsersService(IServiceProvider services)
        {
            _services = services;
            _db = services.GetRequiredService<EFContext>();
            _discord = services.GetRequiredService<DiscordSocketClient>();
        }
        public async Task<User> GetUserAync(ulong id)
        {
            var user = _db.Users.FirstOrDefault(a => a.Id == id);
            if (user is null)
            {
                user = new User()
                {
                    Id = id,
                };
                _db.Users.Add(user);
                await _db.SaveChangesAsync();
            }
            return await Task.FromResult(user);
        }
        public async Task<bool> SendMessageToUserAsync(User user, string text = null, bool isTTS = false, Embed embed = null, RequestOptions options = null, AllowedMentions allowedMentions = null, MessageComponent components = null, Embed[] embeds = null)
        {
            IUser discordUser = await _discord.Rest.GetUserAsync(user.Id);
            try
            {
                await discordUser.SendMessageAsync(text, isTTS, embed, options, allowedMentions, components, embeds);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
