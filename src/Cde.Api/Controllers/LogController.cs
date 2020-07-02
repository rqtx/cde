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
		[HttpGet("system/{systemId}")]
		public ActionResult<List<LogDto>> GetAll(int systemId) {
			return Ok(logService.GetAll(systemId).Select(u => new LogDto() {
				Id = u.Id,
				Title = u.Title,
				Level = u.Level,
				Events = 0
			}).ToList()); ;
		}

		// GET api/<LogController>/5
		[HttpGet("{logId}")]
		public ActionResult<LogModel> Get(int logId) {
			try {
				return Ok(logService.GetById(logId).First());
			} catch (ArgumentNullException) {
				return NotFound("Log not found");
			}
		}

		[HttpGet("system/{systemId}/level/{levelId}")]
		public ActionResult<LogDto> GetByLevel(int systemId, int levelId) { 
			return Ok(logService.GetByLevel(systemId, levelId).Select(u => new LogDto() {
				Id = u.Id,
				Title = u.Title,
				Level = u.Level,
				Events = 0
			}).ToList());
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
