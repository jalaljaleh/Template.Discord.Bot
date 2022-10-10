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
    [Group("params", "params module")]
    public class ParametersModule : CustomInteractionModuleBase<CustomSocketInteractionContext>
    {
        [SlashCommand("number", "test params")]
        public async Task @params(int number)
        {
            await RespondAsync($"Number {number}");
        }
        [SlashCommand("string", "test params")]
        public async Task @string(string text)
        {
            await RespondAsync($"string {text}");
        }

        [SlashCommand("channels", "test params")]
        public async Task channels(IChannel channel, IGuildChannel guildChannel, IVoiceChannel voiceChannel, ITextChannel textChannel, IAudioChannel audioChannel, IForumChannel forumChannel, ICategoryChannel categoryChannel, IGroupChannel group)
        {
            await RespondAsync($"pong");
        }

        [SlashCommand("users", "test params")]
        public async Task users(IUser user, IGuildUser guildUser)
        {
            await RespondAsync($"pong");
        }

        [SlashCommand("file", "test params")]
        public async Task file(IAttachment attachment)
        {
            await RespondAsync($"pong");
        }

        [SlashCommand("role", "test params")]
        public async Task role(IRole role)
        {
            await RespondAsync($"pong");
        }

        public enum Test { Test1, Test2, Test3, Test4, Test5, Test6, Test7, Test8, Test9, Test10 }
        [SlashCommand("enum", "test params")]
        public async Task enums(Test test)
        {
            await RespondAsync($"pong");
        }


        public enum Test1 { [ChoiceDisplay("Test 1")] Test1, [ChoiceDisplay("Custom Test 2")] Test2, Test3, Test4, Test5, Test6, Test7, Test8, Test9, Test10 }
        [SlashCommand("enum-custom", "test params")]
        public async Task custom_enum(Test1 test)
        {
            await RespondAsync($"pong");
        }
    }

}
