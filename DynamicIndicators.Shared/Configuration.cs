using Newtonsoft.Json;
using System.Collections.Generic;

namespace DynamicIndicators.Shared
{
    internal class Configuration
    {
        [JsonProperty("vehicles")]
        List<BlinkerParameters> Vehicles = new();
    }
}
