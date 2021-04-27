using Microsoft.AspNetCore.Mvc;

namespace APIServiceB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        [HttpGet]
        public string Get()
        {
            return "From APIServiceB";
        }

    }
}
