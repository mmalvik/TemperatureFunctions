using Newtonsoft.Json;

namespace TemperatureFunctions.Grafana
{
    public struct TimeSeries
    {
        [JsonProperty("target")]
        public string Target { get; set; }

        [JsonProperty("datapoints")]
        public long[][] Datapoints { get; set; }
    }
}