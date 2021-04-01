using Eatigo.Eatilink.DataObjects.Models;
using Eatigo.Eatilink.Validator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Eatigo.Eatilink.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LinkShortenerController : ControllerBase
    {
        private readonly ILinkShortenerValidator _linkShortenerValidator;
        public LinkShortenerController(ILinkShortenerValidator linkShortenerValidator)
        {
            _linkShortenerValidator = linkShortenerValidator;
        }
        [HttpPost("shorten")]
        public IActionResult LinkShortener(UrlDto model) 
        {
            var(statusCode, errorResult) = _linkShortenerValidator.PayloadValidator(Request.Headers[HeaderNames.Authorization], model.OriginalUrl, model.Domain);
            if (statusCode != StatusCodes.Status200OK) return StatusCode(statusCode, errorResult);
            return StatusCode(StatusCodes.Status200OK, "Hasan");
        }
    }
}
