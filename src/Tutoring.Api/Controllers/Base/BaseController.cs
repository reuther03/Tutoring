using Microsoft.AspNetCore.Mvc;
using TripManager.Common.Primitives.Envelopes;

namespace TripManager.Api.Controllers.Base;

[ApiController]
[Route("[controller]")]
public abstract class BaseController : ControllerBase
{
    protected IActionResult HandleResult()
    {
        return Ok(new Envelope
        {
            StatusCode = 200,
            Data = new EmptyData()
        });
    }

    protected IActionResult HandleResult<T>(T result)
    {
        return Ok(new Envelope
        {
            StatusCode = 200,
            Data = result
        });
    }
}

public sealed class EmptyData;