using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace API.Controllers;

/// <remarks>
/// @Body, @Scripts
/// </remarks>
public class PageBuilder
{
    private string layout;
    private Dictionary<string, string> sections = new();

    private static string? root;
    public string Title { get; init; } = "ХУЙ";
    public bool IsAuthenticated { get; init; } = false;

    public static void SetRoot(string root)
    {
        PageBuilder.root = root;
    }


    public PageBuilder AddLayout(string path)
    {
        var finalPath = Path.Combine(root, path);
        var l = File.ReadAllText(finalPath);
        layout = l;
        return this;
    }

    public PageBuilder AddBody(string path)
    {
        var finalPath = Path.Combine(root, path);
        var body = File.ReadAllText(finalPath);
        sections.Add("Body", body);
        return this;
    }

    public PageBuilder AddScripts( bool head = false, params string[] paths)
    {
        if (paths.Length == 0)
            return this;
        if (!head)
        {        
            var elements = string.Join("\n", paths.Select(f => $"<script type='text/javascript' src='{f}'></script>"));
            sections.Add("Scripts", elements);
        }
        else
        {
            var elements = string.Join("\n", paths.Select(f => $"<script src='{f}'></script>"));

            sections.Add("HeadScripts", elements);
        }
        return this;
    }
    
    public PageBuilder AddStyles(params string[] paths)
    {
        if (paths.Length == 0)
            return this;
        var elements = string.Join("\n", paths.Select(f => $"<link rel='stylesheet' href='{f}'></link>"));
        sections.Add("Styles", elements);
        return this;
    }

    public string LoadEntirePage(string path)
    {
        var finalPath = Path.Combine(root, path);
        return  File.ReadAllText(finalPath);
    }

    public string Build()
    {
        // First handle conditional blocks
        layout = ProcessConditionalBlocks(layout);
        
        // Then handle regular sections
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
    
    private string ProcessConditionalBlocks(string content)
    {
        // Process @if(auth) blocks
        var ifAuthPattern = @"@if\s*\(\s*auth\s*\)\s*\{(.*?)\}\s*(?:@else\s*\{(.*?)\})?";
        return Regex.Replace(content, ifAuthPattern, match =>
        {
            var ifBlock = match.Groups[1].Value;
            var elseBlock = match.Groups.Count > 2 ? match.Groups[2].Value : string.Empty;
            
            return IsAuthenticated ? ifBlock : elseBlock;
        }, RegexOptions.Singleline);
    }
}