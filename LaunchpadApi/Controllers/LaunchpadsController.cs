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
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Launchpad.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class LaunchpadsController : Controller
    {
        private readonly ILaunchpadManager _manager;
        private readonly IMapper _mapper;

        public LaunchpadsController(IMapper mapper, ILaunchpadManager launchpadManager)
        {
            _mapper = mapper;
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _manager.GetLaunchpadById(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<LaunchpadModel>(result));
        }
    }
}
