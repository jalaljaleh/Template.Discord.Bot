﻿using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Template.Data;

namespace Template.Messages
{
    public class MessageHandler
    {
        private readonly IServiceProvider _services;
        private readonly DiscordSocketClient _discord;
        private readonly UsersService usersService;
        private readonly QuestionsService questionsService;
        public MessageHandler(IServiceProvider services)
        {
            _services = services;
            _discord = services.GetRequiredService<DiscordSocketClient>();
            usersService = services.GetRequiredService<UsersService>();
            questionsService = services.GetRequiredService<QuestionsService>();

            _discord.MessageReceived += _discord_MessageReceived;
        }

        private async Task _discord_MessageReceived(SocketMessage rawMessage)
        {
            if (!(rawMessage is SocketUserMessage message))
                return;

            if (message.Source != Discord.MessageSource.User)
                return;

            if (message.Channel.GetChannelType() is not ChannelType.DM)
                return;

            var user = await usersService.GetUserAync(message.Author.Id);

            var result = await questionsService.OnUserResponse(message, user);
            if (result) return;


        }
    }
}