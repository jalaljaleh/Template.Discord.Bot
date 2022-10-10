
using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using global::Template.Data;
using Microsoft.EntityFrameworkCore;
using Template.Commands;
using Template.Messages;

namespace Template
{
    public class Program
    {
        public const ulong DebugServerId = 1026022031621369867;

        public static Random Random = new Random();
        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();
        public async Task MainAsync()
        {
            var configuration = BotConfiguration.GetConfiguration();

            var discordConfig = new DiscordSocketConfig()
            {
                AlwaysDownloadUsers = true,
                GatewayIntents = GatewayIntents.All
            };
            var discord = new DiscordSocketClient(discordConfig);

            var services = new ServiceCollection()
                .AddSingleton(configuration)
                .AddSingleton(discord)

                .AddDbContext<EFContext>(
                x =>
                x.UseSqlServer(@"Data Source=.\sqlexpress;Initial catalog=BotDb;Integrated Security=True"))

                .AddSingleton<LocaleService>()
                .AddSingleton<UsersService>()
                .AddSingleton<GuildService>()

                .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
                .AddSingleton<InteractionsHandler>()

                .AddSingleton<MessageHandler>()
                .AddSingleton<CommandService>()
                .AddSingleton<CommandsHandler>()

                .BuildServiceProvider();

            await RunAsync(services);
        }



        public async Task RunAsync(IServiceProvider _services)
        {
            await Log("DiscordBotService", "Run Async");

            var config = _services.GetRequiredService<BotConfiguration>();

            await _services.GetRequiredService<LocaleService>().InitializeAsync();

            var client = _services.GetRequiredService<DiscordSocketClient>();
            client.Log += Client_Log;

            _services.GetRequiredService<MessageHandler>();

            await _services.GetRequiredService<InteractionsHandler>().InitializeAsync();

            await _services.GetRequiredService<CommandsHandler>().InitializeAsync();

            await client.LoginAsync(TokenType.Bot, config.Token, true);
            await client.StartAsync();

            await Task.Delay(Timeout.Infinite);
        }

        private Task Client_Log(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }


        public static Task Log(string service, string content, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(DateTime.UtcNow.ToString("T") + " " + service.PadRight(10) + content);
            return Task.CompletedTask;
        }

        public static bool IsDebug()
        {
#if DEBUG
            return true;
#else
return false;
#endif
        }
    }
}