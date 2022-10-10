using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
using Template.Interactions;

namespace Discord.Interactions
{
    public class RequireLocale : PreconditionAttribute
    {
        public enum LocaleType { GuildLocale, UserLocale }

        private readonly string[] locales;
        private readonly LocaleType type;
        public RequireLocale(LocaleType type, params string[] locale)
        {
            this.locales = locale;
        }
        public RequireLocale(string locale)
        {
            this.locales = new string[] { locale };
        }
        public override Task<PreconditionResult> CheckRequirementsAsync(IInteractionContext context_, ICommandInfo commandInfo, IServiceProvider services)
        {
            var context = context_ as CustomSocketInteractionContext;

            if (locales.Contains( 
                type == LocaleType.UserLocale
                ? context.Interaction.UserLocale
                : context.Interaction.GuildLocale))
                  return Task.FromResult(PreconditionResult.FromSuccess());

            return Task.FromResult(PreconditionResult.FromError(context.UserLocale["precondination.error.require_locale"]));
        }
    }
}
