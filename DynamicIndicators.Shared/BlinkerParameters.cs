using Newtonsoft.Json;

namespace DynamicIndicators.Shared
{
    internal class BlinkerParameters
    {
        private int _duration;

        [JsonProperty("model")]
        public string ModelName;

        [JsonProperty("duration")]
        public int Duration
        {
            get => _duration;
            set
            {
                _duration = value < 0 ? 0 : value;
            }
        }

        [JsonProperty("debug")]
        public bool Debug;
    }
}
