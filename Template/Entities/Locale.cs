/*!
 * Discord Locale v1.5 (https://jalaljaleh.github.io/)
 * Copyright 2021-2022 Jalal Jaleh
 * Licensed under MIT (https://github.com/jalaljaleh/Template.Discord.Bot/blob/master/LICENSE.txt)
 * Original (https://github.com/jalaljaleh/Template.Discord.Bot/blob/master/Template/Entities/Locale.cs)
 */

namespace Template
{
    public class Locale
    {

        private Dictionary<string, string> items;
        public Locale(Dictionary<string, string> items)
        {
            this.items = items;
        }
        public string this[string key]
        {
            get
            {
                string result;
                if (items.TryGetValue(key, out string value))
                {
                    result = value;
                }
                else
                {
                    result = key;
                };
                return result;
            }
        }
    }
}
