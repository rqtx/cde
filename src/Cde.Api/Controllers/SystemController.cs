using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cde.Api.Constants;
using Cde.Database;
using Cde.Models;
using Microsoft.AspNetCore.Authorization;
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
		private readonly DatabaseService<SystemModel> systemService;

		public SystemController(ApplicationDbContext context) {
			systemService = new DatabaseService<SystemModel>(context);
		}

		// GET: api/<SystemController>
		[HttpGet]
		public ActionResult<List<SystemModel>> Get() {
			return Ok(systemService.GetAll().ToList());
		}

		// GET api/<SystemController>/5
		[HttpGet("{id}")]
		public ActionResult<SystemModel> Get(int id) {
			try {
				return Ok(systemService.Get(l => l.Id == id).First());
			} catch (Exception e) {
				if (e is ArgumentNullException || e is InvalidOperationException) {
					return NotFound("System not found");
				}
				throw e;
			}
		}

		// POST api/<SystemController>
		[HttpPost]
		[Authorize(Roles.Admin)]
		public ActionResult<SystemModel> Post([FromBody] SystemModel system) {
			if (null != systemService.Get(s => s.Name == system.Name).FirstOrDefault()) {
				return Conflict(new { error = "System alredy exist!" });
			}
			return Created("", systemService.Create(system));
		}

		// PUT api/<SystemController>/5
		[HttpPut("{id}")]
		[Authorize(Roles.Admin)]
		public ActionResult Put(int id, [FromBody] SystemModel system) {
			var updatedSys = systemService.Get(u => u.Id == id).First();
			if (null == updatedSys) {
				return NotFound(new { error = "System not found" });
			}
			updatedSys.Id = id;
			updatedSys.Name = system.Name;
			return Ok(systemService.Update(updatedSys));
		}

		// DELETE api/<SystemController>/5
		[HttpDelete("{id}")]
		[Authorize(Roles.Admin)]
		public ActionResult Delete(int id) {
			var system = systemService.Get(u => u.Id == id).First();
			if (null == system) {
				return NotFound(new { error = "System not found" });
			}
			systemService.Delete(system);
			return Ok();
		}
	}
}
