using Discord;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
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
            _db = services.GetRequiredService<EFContext>();
            _discord = services.GetRequiredService<DiscordSocketClient>();
            _services = services;
        }
        public async Task<User> GetOrAddAync(ulong id, bool insertNew = true)
        {
            var user = await _db.Users.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
            if (user is null && insertNew)
            {
                user = new User()
                {
                    Id = id,
                };
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
            }
            return user;
        }
        public void Update(User user)
        {
            _db.Users.Update(user);
        }
        public async Task<IMessage> SendMessageAsync(User user, string text = null, bool isTTS = false, Embed embed = null, RequestOptions options = null, AllowedMentions allowedMentions = null, MessageComponent components = null, Embed[] embeds = null)
        {
            IUser discordUser = _discord.GetUser(user.Id) ?? await _discord.GetUserAsync(user.Id) ?? await _discord.Rest.GetUserAsync(user.Id);
            return discordUser is null
                ? null
                : await discordUser.SendMessageAsync(text, isTTS, embed, options, allowedMentions, components, embeds);
        }

    }
}
