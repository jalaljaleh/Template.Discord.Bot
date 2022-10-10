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
    public class GuildService
    {
        private readonly EFContext _db;
        private readonly DiscordSocketClient _discord;
        private readonly IServiceProvider _services;
        private readonly BotConfiguration _botConfiguration;
        public GuildService(IServiceProvider services)
        {
            _db = services.GetRequiredService<EFContext>();
            _discord = services.GetRequiredService<DiscordSocketClient>();
            _botConfiguration = services.GetRequiredService<BotConfiguration>();

            _services = services;
        }
        public async Task<Guild> GetOrAddAync(ulong id, bool insertNew = true)
        {
            var guild = await _db.Guilds.FirstOrDefaultAsync(a => a.Id == id);
            if (guild is null && insertNew)
            {
                guild = new Guild()
                {
                    Id = id,
                    Prefix = _botConfiguration.Prefix
                };
                await _db.Guilds.AddAsync(guild);
                await _db.SaveChangesAsync();
            }
            return guild;
        }

    }
}
