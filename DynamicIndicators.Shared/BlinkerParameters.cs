using Newtonsoft.Json;

namespace DynamicIndicators.Shared
{
    internal class BlinkerParameters
    {
        [JsonProperty("model")]
        public string ModelName;

        [JsonProperty("duration")]
        public int Duration;

        [JsonProperty("debug")]
        public bool Debug;
    }
}
