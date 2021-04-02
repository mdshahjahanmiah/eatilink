using Eatigo.Eatilink.DataObjects.Models;
using Eatigo.Eatilink.Domain.Interfaces;
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
        private readonly ILinkShortenManager _linkShortenManager;
        public LinkShortenerController(ILinkShortenerValidator linkShortenerValidator, ILinkShortenManager linkShortenManager)
        {
            _linkShortenerValidator = linkShortenerValidator;
            _linkShortenManager = linkShortenManager;
        }
        [HttpPost("shorten")]
        public IActionResult LinkShortener(ShortUrlRequest model) 
        {
            var(statusCode, errorResult) = _linkShortenerValidator.PayloadValidator(Request.Headers[HeaderNames.Authorization], model.OriginalUrl, model.Domain);
            if (statusCode != StatusCodes.Status200OK) return StatusCode(statusCode, errorResult);

            var result = _linkShortenManager.Shorten(model);
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
