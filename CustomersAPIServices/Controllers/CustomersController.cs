using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CustomersAPIServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "thelno Van Amersvoort", "Joakim Bustos" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return $"{id}";
        }
    }
}
