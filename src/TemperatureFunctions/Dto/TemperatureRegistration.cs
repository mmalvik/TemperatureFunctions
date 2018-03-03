using System;
using Newtonsoft.Json;

namespace TemperatureFunctions.Dto
{
    public class TemperatureRegistration
    {
        [JsonProperty("temperature")]
        public int Temperature { get; set; }

        [JsonProperty("timeStamp")]
        public DateTime TimeStamp { get; set; }

        [JsonProperty("_ts")]
        public long Ts { get; set; }
    }
}