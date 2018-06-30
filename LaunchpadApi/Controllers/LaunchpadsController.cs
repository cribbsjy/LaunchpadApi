using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Launchpad.Api.Models;
using Launchpad.Api.Models.Adjustability;
using Launchpad.Core.DTOs;
using Launchpad.Core.Managers.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Launchpad.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class LaunchpadsController : BaseController
    {
        private readonly ILaunchpadManager _manager;

        public LaunchpadsController(ILoggerFactory loggerFactory, 
            IMapper mapper, 
            ILaunchpadManager launchpadManager)
            : base(loggerFactory, 
                  mapper)
        {
            _manager = launchpadManager;
        }

        /// <summary>
        /// Get launchpads based on provided parameters
        /// </summary>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, typeof(IEnumerable<LaunchpadModel>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, typeof(Dictionary<string, string>))]
        public async Task<IActionResult> Get(LaunchpadSearchRequest request)
        {
            _logger.LogInformation($"{nameof(LaunchpadsController)}.{nameof(Get)}", request);
            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"{nameof(LaunchpadsController)}.{nameof(Get)} modelstate invalid", ModelState);
                return BadRequest(ModelState);
            }

            var result = await _manager.GetAllLaunchpads(_mapper.Map<SearchLaunchpadDto>(request));

            return Ok(result.SortBy(request)
                .Paginate(request)
                .FieldSelect(request));
        }

        /// <summary>
        /// Get a specific launchpad by ID
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, typeof(LaunchpadModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string id)
        {
            _logger.LogInformation($"{nameof(LaunchpadsController)}.{nameof(Get)}({id})", id);
            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"{nameof(LaunchpadsController)}.{nameof(Get)}({id}) modelstate invalid", ModelState);
                return BadRequest(ModelState);
            }

            var result = await _manager.GetLaunchpadById(id);
            if (result == null)
            {
                _logger.LogWarning($"{nameof(LaunchpadsController)}.{nameof(Get)}({id}) not found", id);
                return NotFound();
            }

            return Ok(_mapper.Map<LaunchpadModel>(result));
        }
    }
}
