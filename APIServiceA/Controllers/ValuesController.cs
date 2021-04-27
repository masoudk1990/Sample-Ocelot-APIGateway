using Microsoft.AspNetCore.Mvc;

namespace APIServiceA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        [HttpGet]
        public string Get()
        {
            return "From APIServiceA";
        }

    }
}
