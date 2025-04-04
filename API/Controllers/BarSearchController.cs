using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BarSearchController : ControllerBase
{
    private static readonly List<Bar> _bars = BarsController._bars;

    [HttpGet("by-tags")]
    public ActionResult<IEnumerable<int>> GetByTags([FromQuery] List<string> tags)
    {
        if (tags == null || !tags.Any())
        {
            return BadRequest("Должен быть указан хотя бы один тег");
        }

        var matchingBarIds = _bars
            .Where(bar => tags.All(tag => bar.Tags.Contains(tag)))
            .Select(bar => bar.Id)
            .ToList();

        return Ok(matchingBarIds);
    }

    [HttpGet("by-any-tag")]
    public ActionResult<IEnumerable<int>> GetByAnyTag([FromQuery] List<string> tags)
    {
        if (tags == null || !tags.Any())
        {
            return BadRequest("Должен быть указан хотя бы один тег");
        }

        var matchingBarIds = _bars
            .Where(bar => tags.Any(tag => bar.Tags.Contains(tag)))
            .Select(bar => bar.Id)
            .ToList();

        return Ok(matchingBarIds);
    }
}