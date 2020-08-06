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
		private readonly ILevelService _levelService;

		public LevelController(ILevelService levelService) {
			_levelService = levelService;
		}

		// GET: api/<LevelController>
		[HttpGet]
		public ActionResult<IEnumerable<LevelModel>> Get() {
			return Ok(_levelService.GetAll());
		}

		// GET api/<LevelController>/5
		[HttpGet("{id}")]
		public ActionResult<LevelModel> Get(int id) {
			var level = _levelService.Get(l => l.Id == id).FirstOrDefault();
			if (null == level) {
				return NotFound(new { error = "Level not found" });
			}
			return Ok(level);
		}

		// POST api/<LevelController>
		//[HttpPost]
		[AuthorizeRoles(Roles.Admin)]
		public ActionResult<LevelModel> Post([FromBody] LevelModel level) {
			if (null != _levelService.Get(l => l.Name == level.Name).FirstOrDefault()) {
				return Conflict(new { error = "Level alredy exist!" });
			}
			return Created("", _levelService.Create(level));
		}

		// PUT api/<LevelController>/5
		//[HttpPut("{id}")]
		[AuthorizeRoles(Roles.Admin)]
		public ActionResult Put(int id, [FromBody] LevelModel level) {
			var updatedLevel = _levelService.Get(u => u.Id == id).First();
			if (null == updatedLevel) {
				return NotFound(new { error = "Level not found" });
			}
			updatedLevel.Id = id;
			updatedLevel.Name = level.Name;
			return Ok(_levelService.Update(updatedLevel));
		}

		// DELETE api/<LevelController>/5
		//[HttpDelete("{id}")]
		[AuthorizeRoles(Roles.Admin)]
		public ActionResult Delete(int id) {
			var level = _levelService.Get(u => u.Id == id).First();
			if (null == level) {
				return NotFound(new { error = "Level not found" });
			}
			_levelService.Delete(level);
			return Ok();
		}
	}
}
