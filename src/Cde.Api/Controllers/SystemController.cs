using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
		public ActionResult<SystemModel> Post([FromBody] SystemModel system) {
			try {
				systemService.Create(system);
				return Created("", new LevelModel() { Id = system.Id, Level = system.Name });
			} catch (DbUpdateException) {
				return Conflict("System alredy exist");
			}
		}

		// PUT api/<SystemController>/5
		[HttpPut("{id}")]
		public ActionResult Put(int id, [FromBody] SystemModel system) {
			try {
				var updatedSys = systemService.Get(u => u.Id == id).First();
				updatedSys.Id = id;
				updatedSys.Name = system.Name;
				return Ok();
			} catch (Exception e) {
				if (e is ArgumentNullException || e is InvalidOperationException) {
					return NotFound("System not found");
				}
				throw e;
			}
		}

		// DELETE api/<SystemController>/5
		[HttpDelete("{id}")]
		public ActionResult Delete(int id) {
			try {
				var system = systemService.Get(u => u.Id == id).First();
				systemService.Delete(system);
				return Ok();
			} catch (Exception e) {
				if (e is ArgumentNullException || e is InvalidOperationException) {
					return NotFound("System not found");
				}
				throw e;
			}
		}
	}
}
