using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template
{
    public class LocaleService
    {
        private Dictionary<string, Locale> Locales;
        public LocaleService()
        {
            Locales = new Dictionary<string, Locale>();
        }
        public Task InitializeAsync()
        {
            var directoryPath = Directory.GetFiles(@"Resources/Locales");
            foreach (var f in directoryPath)
            {
                var locale = Path.GetFileNameWithoutExtension(f);

                var data = File.ReadAllText(f);
                var json = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);

                Locales.Add(locale, new Locale(json));
            }
            return Task.CompletedTask;
        }

        public bool TryGet(string key, out Locale locale)
        {
            var result = Locales.TryGetValue(key, out locale);
            return result;
        }
        public Locale GetOrDefault(string key)
        {
            var result = TryGet(key, out Locale locale);

            if (locale is null)
                locale = Locales.FirstOrDefault(a => a.Key == "en-US").Value;

            return locale;
        }

    }

}
