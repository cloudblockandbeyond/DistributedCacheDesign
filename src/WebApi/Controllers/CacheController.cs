namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CacheController : ControllerBase
{
    private readonly IMediator _mediator;

    public CacheController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddCacheItemCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result)
            return BadRequest();

        return Ok(result);
    }

    [HttpGet("{key}")]
    public async Task<IActionResult> Get(string key)
    {
        var query = new GetCacheItemQuery(key);
        var result = await _mediator.Send(query);

        if (result == null || string.IsNullOrWhiteSpace(result.Value))
            return NoContent();

        return Ok(result);
    }
}
