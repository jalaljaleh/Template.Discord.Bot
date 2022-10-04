using Discord;

namespace Template.Interactions
{
    using Discord.Interactions;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    

    public class CustomInteractionModuleBase : InteractionModuleBase<CustomSocketInteractionContext>
    {
        public Locale UserLocale { get => Context.UserLocale; }
        public Locale GuildLocale { get => Context.GuildLocale; }
        public CustomInteractionModuleBase()
        {

        }

        public async Task<IUserMessage> LoadingAsync(IUserMessage msg = null, MessageComponent component = null)
        {
            var embed = new EmbedBuilder()
            {
                Title = "Please wait",
                Description = "Please wait, we are working on it.",
                Color = Color.Orange,
                ThumbnailUrl = "https://cdn.discordapp.com/attachments/1022106350219698186/1022529862960947200/4.gif",
            }.Build();

            var action = msg is not null
                ? ModifyMessageWithEmbed(msg, embed, component)
               : FollowupAsync(embed: embed, components: component);

            var result = await action;
            return result;
        }
        public enum ReplyType
        {
            Success,
            Danger,
            Info
        }
        public async Task<IUserMessage> ReplyAsync(ReplyType replyType, string content, string description, string title, MessageComponent component = null)
        {
            var embed = new EmbedBuilder()
            {
                Title = title,
                Description = description,
                Color = replyType != ReplyType.Info ? replyType == ReplyType.Success ? Color.Green : Color.Red : Color.Orange
            }.Build();
            return await base.ReplyAsync(content, embed: embed, components: component);
        }
        public async Task<IUserMessage> FinishedAsync(IUserMessage msg = null, MessageComponent component = null)
        {
            var embed = new EmbedBuilder()
            {
                Title = "The operation was completed",
                Description = "The operation was completed successfully.",
                Color = Color.Green,
                ThumbnailUrl = "https://img.icons8.com/fluency/200/good-quality.png",
            }.Build();

            var action = msg is not null
               ? ModifyMessageWithEmbed(msg, embed, component)
               : FollowupAsync(embed: embed, components: component);

            var result = await action;
            return result;
        }

        private async Task<IUserMessage> ModifyMessageWithEmbed(IUserMessage msg, Embed embed = null, MessageComponent component = null)
        {
            await msg.ModifyAsync(x =>
            {
                x.Content = null;
                x.Embed = embed;
                x.Embeds = new Optional<Embed[]>();
                x.Components = component is not null ? component : new ComponentBuilder().Build();
                x.Attachments = new Optional<IEnumerable<FileAttachment>>();
            });
            return msg;
        }
    }
}
