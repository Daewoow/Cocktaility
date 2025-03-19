using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace API.Controllers;
/// <remarks>
/// @Body, @Scripts
/// </remarks>
public class PageBuilder
{
    private string result = "";
    private string layout;
    private Dictionary<string, string> sections = new();
    public string Title { get; init; } = "ХУЙ";
    public string Root { get; init; } = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName, "API/wwwroot");
    
    public PageBuilder AddLayout(string path)
    {
        var finalPath = Path.Combine(Root, path);
        var l = File.ReadAllText(finalPath);
        layout = l;
        return this;
    }

    public PageBuilder AddBody(string path)
    {
        var finalPath = Path.Combine(Root, path);
        var body = File.ReadAllText(finalPath);
        sections.Add("Body", body);
        return this;
    }

    public PageBuilder AddScripts(params string[] paths)
    {
        if (paths.Length == 0)
            return this;
        var elements = String.Join("\n", paths.Select(f => $"<script type='text/javascript' src='{f}'></script>"));
        sections.Add("Scripts", elements);
        return this;
    }
    
    public string Build()
    {
        var pattern = @"(@[a-zA-Zа-яА-Я]+\b)";
        string result = Regex.Replace(layout, pattern, match =>
        {
            string word = match.Groups[1].Value;
            if (!sections.ContainsKey(word[1..]))
                return "";
            return sections[word[1..]];
        });
        return result;
    }
}