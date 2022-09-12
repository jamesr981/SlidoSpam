using Newtonsoft.Json;

namespace SlidoSpam.Responses;

public class AuthResponse
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }

    [JsonProperty("event_id")]
    public long EventId { get; set; }

    [JsonProperty("event_user_id")]
    public long EventUserId { get; set; }
}