using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cde.Models;
using Cde.Database;
using Cde.Database.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Cde.Models.DTOs;
using Cde.Api.Constants;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cde.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class LogController : ControllerBase {
		private readonly LogService logService;
		private readonly DatabaseService<LevelModel> levelService;
		private readonly DatabaseService<SystemModel> systemService;
		private readonly IMapper _mapper;

		public LogController(ApplicationDbContext context, IMapper mapper) {
			logService = new LogService(context);
			levelService = new DatabaseService<LevelModel>(context);
			systemService = new DatabaseService<SystemModel>(context);
			_mapper = mapper;
		}

		// GET: api/<LogController>
		[HttpGet("system/{systemId}")]
		public ActionResult<IEnumerable<LogDTO>> GetAll(int systemId, string sortby = null, string orderby = "asc", int? page = null) {
			const int pageSize = 10;
			var logs = logService.GetPageBySystemId(systemId, pageSize, sortby, orderby, page);
			return Ok(_mapper.Map<List<LogDTO>>(logs));
		}

		[HttpGet("system/{systemId}/level/{levelId}")]
		public ActionResult<IEnumerable<LogDTO>> GetByLevel(int systemId, int levelId, string sortby = null, string orderby = "asc", int? page = null) {
			const int pageSize = 10;
			var logs = logService.GetPageBySystemAndLevel(systemId, levelId, pageSize, sortby, orderby, page);
			return Ok(_mapper.Map<List<LogDTO>>(logs));
		}

		[HttpGet("overview/{systemId}")]
		public ActionResult<IEnumerable<LogOverviewDTO>> GetSystemOverview(int systemId) {
			var log = new List<LogOverviewDTO>();
			foreach (var level in levelService.GetAll().ToList()) {
				var logAux = logService.GetRecentByLevel(systemId, level.Id);

				if (logAux == null) {
					continue;
				}

				var overview = new LogOverviewDTO() { 
					Log = _mapper.Map<LogDTO>(logAux),
					Events = 0
				};

				overview.Events = logService.CountEvents(systemId, level.Id);
				log.Add(overview);
			}
			return Ok(log);
		}

		// GET api/<LogController>/5
		[HttpGet("{logId}")]
		public ActionResult<LogDTO> GetById(int logId) {
			var log = logService.GetById(logId);
			if (null == log) {
				return NotFound(new { error = "Log not found" });
			}
			return Ok(_mapper.Map<LogDTO>(log));
		}

		// POST api/<LogController>
		[HttpPost]
		[Authorize(Roles = Roles.System)]
		public ActionResult Post([FromBody] LogDTO logForm) {
			var level = levelService.Get(l => l.Name == logForm.LevelName).FirstOrDefault();
			var system = systemService.Get(s => s.Name == logForm.SystemName).FirstOrDefault();
			if (null == level || null == system) {
				return BadRequest(new {error = $"{(null == level ? "Level" : "System")} does not exist" });
			}
			var log = new LogModel() { 
				Title = logForm.Title,
				Details = logForm.Details,
				LevelId = level.Id,
				SystemId = system.Id,
				CreatedAt = (logForm.CreatedAt == null ? DateTime.UtcNow : logForm.CreatedAt)
			};
			logService.Create(log);
			return Created("", null);
		}

		// DELETE api/<LogController>/5
		[HttpDelete("{id}")]
		public ActionResult Delete(int id) {
			var log = logService.Get(u => u.Id == id).First();
			if (null == log) {
				return NotFound(new { error = "Log not found" });
			}
			logService.Delete(log);
			return Ok();
		}
	}
}
