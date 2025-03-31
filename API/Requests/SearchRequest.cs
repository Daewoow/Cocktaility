namespace API.Requests;

public class SearchRequest
{
    // Тэги в бд хранятся в виде #tagName
    public IEnumerable<string> Tags { get; set; } = new List<string>();
}