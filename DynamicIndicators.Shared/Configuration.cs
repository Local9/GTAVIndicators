using DynamicIndicators.FiveM.Client;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DynamicIndicators.Shared
{
    internal class Configuration
    {
        private static Configuration _configuration;

        [JsonProperty("vehicles")]
        public List<BlinkerParameters> Vehicles = new();

        internal static void Load()
        {
#if FIVEM
            string configFile = LoadResourceFile(GetCurrentResourceName(), "config.json");
            if (string.IsNullOrEmpty(configFile))
            {
                Main.Log.Error($"Failed to load config.json. Please inform the server owner that the config file is missing or corrupt.");
                return;
            }
            _configuration = JsonConvert.DeserializeObject<Configuration>(configFile);
#endif
        }

#if FIVEM
        public static Configuration GetConfig()
        {
            if (_configuration is null)
                Load();
            
            return _configuration;
        }
#endif
    }
}
