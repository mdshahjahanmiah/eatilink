using Eatigo.Eatilink.DataObjects.Models;
using Eatigo.Eatilink.Domain.Interfaces;
using Eatigo.Eatilink.Validator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace Eatigo.Eatilink.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LinkShortenerController : ControllerBase
    {
        private readonly ILinkShortenerValidator _linkShortenerValidator;
        private readonly ILinkShortenManager _linkShortenManager;
        private readonly ILogger<LinkShortenerController> _logger;
        public LinkShortenerController(ILinkShortenerValidator linkShortenerValidator, ILinkShortenManager linkShortenManager, ILogger<LinkShortenerController> logger)
        {
            _linkShortenerValidator = linkShortenerValidator;
            _linkShortenManager = linkShortenManager;
            _logger = logger;
        }
        [HttpPost("shorten")]
        public IActionResult LinkShortener(ShortUrlRequest model) 
        {
            _logger.LogInformation("[Link Shortener] " + JsonConvert.SerializeObject(model));
            var (statusCode, errorResult) = _linkShortenerValidator.PayloadValidator(Request.Headers[HeaderNames.Authorization], model.OriginalUrl, model.Domain);

            _logger.LogWarning("[Link Shortener] " + JsonConvert.SerializeObject(errorResult));
            if (statusCode != StatusCodes.Status200OK) return StatusCode(statusCode, errorResult);

            var result = _linkShortenManager.Shorten(model);
            _logger.LogInformation("[Link Shortener] " + JsonConvert.SerializeObject(result));

            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
