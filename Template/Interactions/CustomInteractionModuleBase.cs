using Discord;

namespace Template.Interactions
{
    using Discord.Interactions;
    using Discord.WebSocket;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;


    public class CustomInteractionModuleBase<T> : InteractionModuleBase<T> where T : class, IInteractionContext
    {
        public CustomInteractionModuleBase() : base()
        {

        }
  
    }
}
