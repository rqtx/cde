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
	[ApiController]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class LogController : ControllerBase {
		private readonly LogService logService;
		private readonly DatabaseService<LevelModel> levelService;

		public LogController(ApplicationContext context) {
			logService = new LogService(context);
			levelService = new DatabaseService<LevelModel>(context);
		}

		// GET: api/<LogController>
		[HttpGet("system/{systemId}")]
		public ActionResult<List<LogModel>> GetAll(int systemId) {
			return Ok(logService.GetAll(systemId).ToList());
		}

		[HttpGet("overview/{systemId}")]
		public ActionResult<List<LogSystemDto>> GetSystemOverview(int systemId) {
			var log = new List<LogSystemDto>();
			foreach (var level in levelService.GetAll().ToList()) {
				var logAux = logService.GetRecentByLevel(systemId, level.Id).Select(l => new LogSystemDto() { 
					Log = l,
					Events = 0
				}).FirstOrDefault();

				if (logAux == null) {
					continue;
				}

				logAux.Events = logService.CountEvents(systemId, level.Id);
				log.Add(logAux);
			}
			return Ok(log);
		}

		// GET api/<LogController>/5
		[HttpGet("{logId}")]
		public ActionResult<LogModel> Get(int logId) {
			try {
				return Ok(logService.GetById(logId).First());
			} catch (Exception e) {
				if (e is ArgumentNullException || e is InvalidOperationException) {
					return NotFound("Log not found");
				}
				throw e;
			}
		}

		[HttpGet("system/{systemId}/level/{levelId}")]
		public ActionResult<LogSystemDto> GetByLevel(int systemId, int levelId) { 
			return Ok(logService.GetByLevel(systemId, levelId).ToList());
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
			} catch (Exception e) {
				if (e is ArgumentNullException || e is InvalidOperationException) {
					return NotFound("Log not found");
				}
				throw e;
			}
		}
	}
}
