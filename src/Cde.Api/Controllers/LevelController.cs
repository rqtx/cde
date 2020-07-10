﻿using System;
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
					return NotFound();
				}
				throw e;
			}
		}

		// POST api/<LevelController>
		[HttpPost]
		public ActionResult<LevelModel> Post([FromBody] LevelModel level) {
			try {
				levelService.Create(level);
				return Created("", new LevelModel() { Id = level.Id, Level = level.Level });
			} catch (DbUpdateException) {
				return Conflict("Level alredy exist");
			}
		}

		// PUT api/<LevelController>/5
		[HttpPut("{id}")]
		public ActionResult Put(int id, [FromBody] LevelModel level) {
			try {
				var updatedLevel = levelService.Get(u => u.Id == id).First();
				updatedLevel.Id = id;
				updatedLevel.Level = level.Level;
				return Ok();
			} catch (Exception e) {
				if (e is ArgumentNullException || e is InvalidOperationException) {
					return NotFound("Level not found");
				}
				throw e;
			}
		}

		// DELETE api/<LevelController>/5
		[HttpDelete("{id}")]
		public ActionResult Delete(int id) {
			try {
				var level = levelService.Get(u => u.Id == id).First();
				levelService.Delete(level);
				return Ok();
			} catch (Exception e) {
				if (e is ArgumentNullException || e is InvalidOperationException) {
					return NotFound("Level not found");
				}
				throw e;
			}
		}
	}
}
