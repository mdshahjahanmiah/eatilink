using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eatigo.Eatilink.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LinkShortenerController : ControllerBase
    {
        [HttpGet("shorten")]
        public IActionResult LinkShortener() 
        {
            return StatusCode(StatusCodes.Status200OK, "Hasan");
        }
    }
}
