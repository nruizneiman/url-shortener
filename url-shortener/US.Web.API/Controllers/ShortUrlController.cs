using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using US.Domain.Entities;
using US.IService.ShortUrl;
using US.IService.ShortUrl.DTOs;

namespace US.Web.API.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ShortUrlController : ControllerBase
    {
        private readonly IShortUrlService _shortUrlService;
        private readonly ILogger<ShortUrlController> _logger;

        public ShortUrlController(ILogger<ShortUrlController> logger, IShortUrlService shortUrlService)
        {
            _logger = logger;
            _shortUrlService = shortUrlService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<ShortUrlDto> shortUrls = _shortUrlService.GetCollectionFromDataStore();

            return Ok(shortUrls);
        }

        [HttpGet("{shorturl}", Name = "Get")]
        public IActionResult Get(string shorturl, [FromQuery(Name = "redirect")] bool redirect = true)
        {
            try
            {
                ShortUrlDto shortUrl = _shortUrlService.GetItemFromDataStore(shorturl);

                if (shortUrl != null)
                {
                    if (redirect)
                    {
                        return Redirect(shortUrl.LongURL);
                    }
                    else
                    {
                        return Ok(shortUrl);
                    }
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
            }

            return BadRequest(ModelState.Values);
        }
    }
}