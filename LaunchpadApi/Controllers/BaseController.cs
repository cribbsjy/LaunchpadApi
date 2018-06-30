using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Launchpad.Api.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly ILogger _logger;
        protected readonly IMapper _mapper;

        public BaseController(ILoggerFactory loggerFactory, IMapper mapper)
        {
            _logger = loggerFactory.CreateLogger("Controllers");
            _mapper = mapper;
        }
    }
}
