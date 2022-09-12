using Newtonsoft.Json;

namespace SlidoSpam.Responses;

public class UserResponse
{
    [JsonProperty("date_created")]
    public DateTime DateCreated { get; set; }
    
    [JsonProperty("date_signin")]
    public DateTime? DateSignin { get; set; }
    
    [JsonProperty("date_updated")]
    public DateTime DateUpdated { get; set; }
    
    [JsonProperty("event_id")]
    public long EventId { get; set; }
    
    [JsonProperty("event_user_id")]
    public long EventUserId { get; set; }
    
    [JsonProperty("user_hash")]
    public string UserHash { get; set; }
}