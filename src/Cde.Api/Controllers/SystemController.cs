using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cde.Api;
using Cde.Api.Constants;
using Cde.Database;
using Cde.Database.IServices;
using Cde.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cde.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class SystemController : ControllerBase
	{
		private readonly ISystemService _systemService;

		public SystemController(ISystemService systemService) {
			_systemService = systemService;
		}

		// GET: api/<SystemController>
		[HttpGet]
		public ActionResult<IEnumerable<SystemModel>> Get() {
			return Ok(_systemService.GetAll());
		}

		// GET api/<SystemController>/5
		[HttpGet("{id}")]
		public ActionResult<SystemModel> Get(int id) {
			var system = _systemService.Get(l => l.Id == id).FirstOrDefault();
			if (null == system) {
				return NotFound("System not found");
			}
			return Ok(system);
		}

		// POST api/<SystemController>
		[HttpPost]
		[AuthorizeRoles(Roles.Admin)]
		public ActionResult<SystemModel> Post([FromBody] SystemModel system) {
			if (null != _systemService.Get(s => s.Name == system.Name).FirstOrDefault()) {
				return Conflict(new { error = "System alredy exist!" });
			}
			return Created("", _systemService.Create(system));
		}

		// PUT api/<SystemController>/5
		[HttpPut("{id}")]
		[AuthorizeRoles(Roles.Admin)]
		public ActionResult Put(int id, [FromBody] SystemModel system) {
			var updatedSys = _systemService.Get(u => u.Id == id).First();
			if (null == updatedSys) {
				return NotFound(new { error = "System not found" });
			}
			updatedSys.Id = id;
			updatedSys.Name = system.Name;
			return Ok(_systemService.Update(updatedSys));
		}

		// DELETE api/<SystemController>/5
		[HttpDelete("{id}")]
		[AuthorizeRoles(Roles.Admin)]
		public ActionResult Delete(int id) {
			var system = _systemService.Get(u => u.Id == id).First();
			if (null == system) {
				return NotFound(new { error = "System not found" });
			}
			try {
				_systemService.Delete(system);
			} catch { 
				return StatusCode(StatusCodes.Status500InternalServerError, "Cannot delete system");
			}
			return Ok();
		}
	}
}
