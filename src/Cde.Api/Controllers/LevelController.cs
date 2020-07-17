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
	public class LevelController : ControllerBase
	{
		private readonly DatabaseService<LevelModel> levelService;

		public LevelController(ApplicationDbContext context) {
			levelService = new DatabaseService<LevelModel>(context);
		}

		// GET: api/<LevelController>
		[HttpGet]
		public ActionResult<List<LevelModel>> Get() {
			return Ok(levelService.GetAll().ToList());
		}

		// GET api/<LevelController>/5
		[HttpGet("{id}")]
		public ActionResult<LevelModel> Get(int id) {
			try {
				return Ok(levelService.Get(l => l.Id == id).First());
			} catch (Exception e) {
				if (e is ArgumentNullException || e is InvalidOperationException) {
					return NotFound(new { error = "Level not found" });
				}
				throw e;
			}
		}

		// POST api/<LevelController>
		[HttpPost]
		[Authorize(Roles.Admin)]
		public ActionResult<LevelModel> Post([FromBody] LevelModel level) {
			if (null != levelService.Get(l => l.Name == level.Name).FirstOrDefault()) {
				return Conflict(new { error = "Level alredy exist!" });
			}
			return Created("", levelService.Create(level));
		}

		// PUT api/<LevelController>/5
		[HttpPut("{id}")]
		[Authorize(Roles.Admin)]
		public ActionResult Put(int id, [FromBody] LevelModel level) {
			var updatedLevel = levelService.Get(u => u.Id == id).First();
			if (null == updatedLevel) {
				return NotFound(new { error = "Level not found" });
			}
			updatedLevel.Id = id;
			updatedLevel.Name = level.Name;
			return Ok(levelService.Update(updatedLevel));
		}

		// DELETE api/<LevelController>/5
		[HttpDelete("{id}")]
		[Authorize(Roles.Admin)]
		public ActionResult Delete(int id) {
			var level = levelService.Get(u => u.Id == id).First();
			if (null == level) {
				return NotFound(new { error = "Level not found" });
			}
			levelService.Delete(level);
			return Ok();
		}
	}
}
