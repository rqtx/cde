using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cde.Models;
using Cde.Database;
using Cde.Database.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cde.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LogController : ControllerBase {
		private readonly LogService logService;

		public LogController(ApplicationContext context) {
			logService = new LogService(context);
		}

		// GET: api/<LogController>
		[HttpGet("{systemId}/{branchStr}")]
		public ActionResult<List<LogDto>> GetAll(int systemId, string branchStr) {
			Branch branch;
			try {
				branch = (Branch)Enum.Parse(typeof(Branch), branchStr, true);
			} catch (Exception) {
				return BadRequest($"Branch {branchStr} does not exist!");
			}
			return Ok(logService.GetAll(systemId, branch).Select(u => new LogDto() { 
						Id = u.Id,
						Title = u.Title,
						Level = u.Level,
						Events = logService.CountEvents(u.SystemId, u.Level, u.Branch)
					}).ToList());
		}

		// GET api/<LogController>/5
		[HttpGet("{id}")]
		public ActionResult<LogModel> Get(int id) {
			try {
				return Ok(logService.Get(l => l.Id == id).First());
			} catch (ArgumentNullException) {
				return NotFound("Log not found");
			}
		}

		// POST api/<LogController>
		[HttpPost]
		public ActionResult Post([FromBody] LogModel log) {
			logService.Create(log);
			return Created("", log);
		}

		// DELETE api/<LogController>/5
		[HttpDelete("{id}")]
		public ActionResult Delete(int id) {
			try {
				var log = logService.Get(u => u.Id == id).First();
				logService.Delete(log);
				return Ok();
			} catch (ArgumentNullException) {
				return NotFound("Log not found");
			}
		}
	}
}
