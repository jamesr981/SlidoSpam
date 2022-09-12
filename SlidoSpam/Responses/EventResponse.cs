using Newtonsoft.Json;

namespace SlidoSpam.Responses;

public class EventResponse
{
    [JsonProperty("event_id")]
    public long EventId { get; set; }

    [JsonProperty("uuid")]
    public string Uuid { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("code")] 
    public string Code { get; set; }

    [JsonProperty("sections")] public Section[] Sections { get; set; } = Array.Empty<Section>();
}

public class Section
{
    [JsonProperty("event_id")]
    public long EventId { get; set; }
    
    [JsonProperty("event_section_id")]
    public long EventSectionId { get; set; }
    
    [JsonProperty("is_active")]
    public bool IsActive { get; set; }
    
    [JsonProperty("is_deleted")]
    public bool IsDeleted { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("uuid")]
    public string Uuid { get; set; }
    
    [JsonProperty("order")]
    public int Order { get; set; }
}