using Newtonsoft.Json;

namespace GlobalApi.Models.Authentication
{
    public class FacebookTookenvalidationResult
    {
        [JsonProperty]
        public FacebookTookenvalidationData data { get; set; }
    }
    public class FacebookTookenvalidationData
    {
        [JsonProperty("AppId")]
        public string AppId { get; set; }
        [JsonProperty("Type")]
        public string Type { get; set; }
        [JsonProperty("Application")]
        public string Application { get; set; }
        [JsonProperty("DataAccessExpiresAt")]
        public long DataAccessExpiresAt { get; set; }
        [JsonProperty("ExpiresAt")]
        public long ExpiresAt { get; set; }
        [JsonProperty("Isvalid")]
        public bool Isvalid { get; set; }
        [JsonProperty("Scopes")]
        public string[] Scopes { get; set; }
        [JsonProperty("userId")]
        public string userId { get; set; }

    }
}
