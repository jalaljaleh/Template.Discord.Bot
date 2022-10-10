using Newtonsoft.Json;

namespace Template
{
    public class BotConfiguration
    {
        public string Token { get; set; }
        public static BotConfiguration GetConfiguration()
        {
            var data = File.ReadAllText(@"config.json");
            return JsonConvert.DeserializeObject<BotConfiguration>(data);
        }
    }
}