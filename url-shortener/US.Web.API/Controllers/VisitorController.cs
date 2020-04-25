using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using US.IService.Visitor;
using US.IService.Visitor.DTOs;

namespace US.Web.API.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        private readonly IVisitorService _visitorService;
        private readonly ILogger<VisitorController> _logger;

        public VisitorController(ILogger<VisitorController> logger, IVisitorService visitorService)
        {
            _logger = logger;
            _visitorService = visitorService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<VisitorDto> visitors = _visitorService.GetAll();

            return Ok(visitors);
        }
    }
}