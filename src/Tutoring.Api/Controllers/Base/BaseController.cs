using Microsoft.AspNetCore.Mvc;
using Tutoring.Common.Primitives;
using Tutoring.Common.Primitives.Envelopes;

namespace Tutoring.Api.Controllers.Base;

[ApiController]
[Route("[controller]")]
public abstract class BaseController : ControllerBase
{

    protected IActionResult HandleResult(Result result)
    {
        return Ok(new Envelope
        {
            StatusCode = result.StatusCode,
            Data = new EmptyData(),
            Error = result.Message
        });
    }

    protected IActionResult HandleResult<T>(Result<T> result)
    {
        return Ok(new Envelope
        {
            StatusCode = result.StatusCode,
            Data = result,
            Error = result.Message
        });
    }
}

public sealed class EmptyData;