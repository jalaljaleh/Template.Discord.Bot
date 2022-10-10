using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Template.Data;
using Template.Interactions;

namespace Template
{
    public class InteractionsHandler
    {
        private readonly IServiceProvider services;
        private readonly InteractionService _interactions;
        private readonly DiscordSocketClient _client;
        private readonly BotConfiguration _config;
        private readonly LocaleService _localeService;
        private readonly UsersService _usersService;
        public InteractionsHandler(IServiceProvider services)
        {
            this.services = services;
            _interactions = services.GetRequiredService<InteractionService>();
            _client = services.GetRequiredService<DiscordSocketClient>();
            _config = services.GetRequiredService<BotConfiguration>();
            _localeService = services.GetRequiredService<LocaleService>();
            _usersService = services.GetRequiredService<UsersService>();
        }
        public async Task InitializeAsync()
        {
            await _interactions.AddModulesAsync(Assembly.GetExecutingAssembly(), services);

            _client.Ready += _client_Ready;
            async Task _client_Ready()
            {
                if (Program.IsDebug())
                    await _interactions.RegisterCommandsToGuildAsync(Program.DebugServerId, true);
                else
                    await _interactions.RegisterCommandsGloballyAsync(true);
            }

            _client.InteractionCreated += _client_IntegrationCreated;
            _interactions.InteractionExecuted += _interactions_InteractionExecuted;
        }

        private async Task _client_IntegrationCreated(SocketInteraction interaction)
        {
            if (DiscordInput.IsFromDiscordInput(interaction)) return;

            var ctx = new CustomSocketInteractionContext(_client, interaction, _localeService, _usersService);
            await _interactions.ExecuteCommandAsync(ctx, services);
        }


        private async Task _interactions_InteractionExecuted(ICommandInfo info, Discord.IInteractionContext context_, IResult result)
        {
            if (result.IsSuccess)
                return;

            if (context_.Interaction.HasResponded)
                await context_.Interaction.FollowupAsync(result.ErrorReason, ephemeral: true);
            else
                await context_.Interaction.RespondAsync(result.ErrorReason, ephemeral: true);

        }
    }
}