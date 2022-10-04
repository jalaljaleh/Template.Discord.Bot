using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;

namespace Template.Interactions.Precondinations
{
    public class RequireLocale : PreconditionAttribute
    {
        private readonly string[] locale;
        public RequireLocale(params string[] locale)
        {
            this.locale = locale;
        }
        public RequireLocale(string locale)
        {
            this.locale = new string[] { locale };
        }
        public override Task<PreconditionResult> CheckRequirementsAsync(IInteractionContext context_, ICommandInfo commandInfo, IServiceProvider services)
        {
            var context = context_ as CustomSocketInteractionContext;

            if (locale.Contains(context.Interaction.UserLocale))
                return Task.FromResult(PreconditionResult.FromSuccess());
            else return Task.FromResult(PreconditionResult.FromError(context.UserLocale["precondination.error.require_locale"]));
        }
    }
}
