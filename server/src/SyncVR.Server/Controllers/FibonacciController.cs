using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using SyncVR.Server.Models;
using SyncVR.Server.Services;
using SyncVR.Server.Stores;

[ApiController]
[Route("[controller]")]
public class FibonacciController : ControllerBase
{
    private readonly Fibonacci _fibonacci;
    private readonly QueriesStore _queries;
    private readonly ILogger<FibonacciController> _logger;

    public FibonacciController(
        Fibonacci fibonacci,
        QueriesStore queriesStore,
        ILogger<FibonacciController> logger)
    {
        _fibonacci = fibonacci;
        _queries = queriesStore;
        _logger = logger;
    }

    [HttpGet("history")]
    [ProducesResponseType(typeof(IEnumerable<FibonacciQuery>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetQueriesHistoryAsync()
    {
        try
        {
            var queries = await _queries.GetQueriesAsync();
            return Ok(queries);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not serve stored queries.");
            return StatusCode(500, new ErrorResponse
            {
                Error = "Something went wrong on our side.",
            });
        }
    }

    [HttpGet("calculate/{position}")]
    [ProducesResponseType(typeof(FibonacciQuery), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Calculate(
        [FromRoute] int position,
        [FromHeader(Name = "Client-Id")] string? clientId = null)
    {
        if (position < 0)
        {
            return BadRequest(new ErrorResponse
            {
                Error = "Route parameter <position> must be greater than or equal to 0.",
            });
        }

        var fib = await _fibonacci.CalculateAsync(position);

        try
        {
            var query = await _queries.AddQueryAsync(new FibonacciQuery
            {
                Position = position,
                Value = fib,
                ClientId = GetClientId(HttpContext),
            });

            return Ok(query);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not save query.");

            return StatusCode(500, new ErrorResponse
            {
                Error = "Something went wrong on our side.",
            });
        }
    }

    private static string GetClientId(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue("Client-Id", out var clientIdHeader) &&
            clientIdHeader.ToString() is string clientId &&
            !string.IsNullOrEmpty(clientId))
        {
            return Hash(clientId);
        }

        if (context.Connection.RemoteIpAddress?.ToString() is string clientIp &&
            !string.IsNullOrEmpty(clientIp))
        {
            return Hash(clientIp);
        }

        return "anonymous";

        static string Hash(string value)
        {
            using var sha1 = SHA1.Create();
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(value));
            return BitConverter.ToString(hash).Replace("-", string.Empty);
        }
    }
}
