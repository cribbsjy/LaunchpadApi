using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Launchpad.Api.Models;
using Launchpad.Api.Models.Adjustability;
using Launchpad.Core.Managers.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Launchpad.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class LaunchpadsController : Controller
    {
        private readonly ILaunchpadManager _manager;

        public LaunchpadsController(ILaunchpadManager launchpadManager)
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _manager.GetAllLaunchpads();

            return Ok(result.SortBy(request)
                .Paginate(request)
                .FieldSelect(request));
        }

        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, typeof(LaunchpadModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, typeof(string))]
        public async Task<IActionResult> Get(string id)
        {
            throw new NotImplementedException();
        }
    }
}
