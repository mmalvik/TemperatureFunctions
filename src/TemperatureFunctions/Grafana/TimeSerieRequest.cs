using System;
using Newtonsoft.Json;

namespace TemperatureFunctions.Grafana
{
    public partial class TimeSerieRequest
    {
        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("panelId")]
        public long PanelId { get; set; }

        [JsonProperty("range")]
        public Range Range { get; set; }

        [JsonProperty("rangeRaw")]
        public Raw RangeRaw { get; set; }

        [JsonProperty("interval")]
        public string Interval { get; set; }

        [JsonProperty("intervalMs")]
        public long IntervalMs { get; set; }

        [JsonProperty("targets")]
        public Target[] Targets { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("maxDataPoints")]
        public long MaxDataPoints { get; set; }

        [JsonProperty("scopedVars")]
        public ScopedVars ScopedVars { get; set; }

        public static TimeSerieRequest FromJson(string json) => JsonConvert.DeserializeObject<TimeSerieRequest>(json);
    }

    public partial class Range
    {
        [JsonProperty("from")]
        public DateTimeOffset From { get; set; }

        [JsonProperty("to")]
        public DateTimeOffset To { get; set; }

        [JsonProperty("raw")]
        public Raw Raw { get; set; }
    }

    public partial class Raw
    {
        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }
    }

    public partial class ScopedVars
    {
        [JsonProperty("__interval")]
        public Interval Interval { get; set; }

        [JsonProperty("__interval_ms")]
        public IntervalMs IntervalMs { get; set; }
    }

    public partial class Interval
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public partial class IntervalMs
    {
        [JsonProperty("text")]
        public long Text { get; set; }

        [JsonProperty("value")]
        public long Value { get; set; }
    }

    public partial class Target
    {
        [JsonProperty("target")]
        public string TargetTarget { get; set; }

        [JsonProperty("refId")]
        public string RefId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public static class Serialize
    {
        public static string ToJson(this TimeSerieRequest self) => JsonConvert.SerializeObject(self);
    }
}