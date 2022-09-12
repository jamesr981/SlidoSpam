using Newtonsoft.Json;

namespace SlidoSpam.Responses;

public class QuestionResponse
{
    [JsonProperty("attrs")]
    public Attributes Attributes { get; set; }

    [JsonProperty("author")]
    public Author Author { get; set; } = new();

    [JsonProperty("date_created")]
    public DateTime DateCreated { get; set; }
    
    [JsonProperty("date_deleted")]
    public DateTime? DateDeleted { get; set; }
    
    [JsonProperty("date_highlighted")]
    public DateTime? DateHighlighted { get; set; }
    
    [JsonProperty("date_published")]
    public DateTime? DatePublished { get; set; }
    
    [JsonProperty("date_updated")]
    public DateTime DateUpdated { get; set; }

    [JsonProperty("event_id")]
    public long EventId { get; set; }

    [JsonProperty("event_question_id")]
    public long EventQuestionId { get; set; }
    
    [JsonProperty("event_section_id")]
    public long EventSectionId { get; set; }
    
    [JsonProperty("event_user_id")]
    public long EventUserId { get; set; }
    
    [JsonProperty("is_anonymous")]
    public bool IsAnonymous { get; set; }
    
    [JsonProperty("is_answered")]
    public bool IsAnswered { get; set; }
    
    [JsonProperty("is_bookmarked")]
    public bool IsBookmarked { get; set; }
    
    [JsonProperty("is_highlighted")]
    public bool IsHighlighted { get; set; }
    
    [JsonProperty("is_public")]
    public bool IsPublic { get; set; }

    //Unknown
    [JsonProperty("labels")]
    public string[] Labels { get; set; } = Array.Empty<string>();

    [JsonProperty("path")]
    public string Path { get; set; } = "/questions";
    
    [JsonProperty("score")]
    public int Score { get; set; }
    
    [JsonProperty("score_negative")]
    public int ScoreNegative { get; set; }
    
    [JsonProperty("score_positive")]
    public int ScorePositive { get; set; }
    
    [JsonProperty("text")]
    public string Text { get; set; }
    
    [JsonProperty("text_formatted")]
    public string TextFormatted { get; set; }

    [JsonProperty("type")] 
    public string Type { get; set; } = "Question";

}

public class Attributes
{
    [JsonProperty("is_comment")]
    public bool IsComment { get; set; }

    //Does question contain profanity
    [JsonProperty("is_profane")]
    public bool IsProfane { get; set; }

    [JsonProperty("language")]
    public string Language { get; set; }

    [JsonProperty("profanity_metadata")]
    public ProfanityMetaData[] ProfanityMetaData { get; set; } = Array.Empty<ProfanityMetaData>();
}

public class ProfanityMetaData
{
    [JsonProperty("swearword")]
    public string Swearword { get; set; }

    [JsonProperty("first")]
    public int First { get; set; }
    
    [JsonProperty("last")]
    public int Last { get; set; }
}

//Need user data
public class Author
{
}