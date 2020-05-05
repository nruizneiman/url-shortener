using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using US.IService.ShortUrl;
using US.IService.ShortUrl.DTOs;
using US.IService.Visitor;
using US.IService.Visitor.DTOs;

namespace US.Web.API.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ShortUrlController : ControllerBase
    {
        private readonly IShortUrlService _shortUrlService;
        private readonly IVisitorService _visitorService;
        private readonly ILogger<ShortUrlController> _logger;

        public ShortUrlController(ILogger<ShortUrlController> logger, IShortUrlService shortUrlService, IVisitorService visitorService)
        {
            _logger = logger;
            _shortUrlService = shortUrlService;
            _visitorService = visitorService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<ShortUrlDto> shortUrls = _shortUrlService.GetCollectionFromDataStore();

            return Ok(shortUrls);
        }

        [HttpGet("{shorturl}", Name = "Get")]
        public async Task<IActionResult> Get(string shorturl,[FromQuery(Name = "ipAdress")]string ipAdress = null, [FromQuery(Name = "redirect")] bool redirect = true)
        {
            try
            {
                ShortUrlDto shortUrl = _shortUrlService.GetItemFromDataStore(shorturl);

                if (shortUrl != null)
                {
                    var userAgent = HttpContext.Request.Headers["User-Agent"];
                    string parsedUserAgent = Convert.ToString(userAgent[0]);

                    var ip = ipAdress ?? GetIpAddress();

                    VisitorRequestDto visitorRequest = new VisitorRequestDto
                    {
                        Ip = ip,
                        ShortUrl = shorturl,
                        UserAgent = parsedUserAgent
                    };

                    VisitorResponseDto visitorResponse = await _visitorService.RegisterVisitor(visitorRequest);

                    if (visitorResponse.Success)
                    {
                        if (redirect)
                        {
                            return Redirect(shortUrl.LongURL);
                        }

                        return Ok(shortUrl);
                    }

                    return BadRequest("Can't register visitor");
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] ShortUrlRequestDto request)
        {
            if (ModelState.IsValid)
            {
                ShortUrlResponseDto result = _shortUrlService.SaveItemToDataStore(request);

                if (result != null)
                    return Ok(result);
                //return CreatedAtAction(nameof(Get), new {result.ShortURL}, result);
            }

            return BadRequest(ModelState.Values);
        }

        private string GetIpAddress()
        {
            return HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString();
        }
    }
}