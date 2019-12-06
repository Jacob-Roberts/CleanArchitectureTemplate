using Microsoft.AspNetCore.Mvc;

namespace Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
    }
}
