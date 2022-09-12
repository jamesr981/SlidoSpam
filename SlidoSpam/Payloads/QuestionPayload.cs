using Newtonsoft.Json;

namespace SlidoSpam.Payloads;

public class QuestionPayload
{
    [JsonProperty("event_id")]
    public long EventId { get; set; }
    
    [JsonProperty("event_section_id")]
    public long EventSectionId { get; set; }
    
    [JsonProperty("is_anonymous")]
    public bool IsAnonymous { get; set; }

    [JsonProperty("labels")]
    public string[] Labels { get; set; } = Array.Empty<string>();

    [JsonProperty("path")] 
    public string Path { get; set; } = "/questions";
    
    [JsonProperty("text")] 
    public string Text { get; set; }
    

}