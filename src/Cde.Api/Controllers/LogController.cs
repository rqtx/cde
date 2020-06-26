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
	public class LogController : ControllerBase
	{
		private readonly DatabaseService<LogModel> logService;

		public LogController(ApplicationContext context) {
			logService = new DatabaseService<LogModel>(context);
		}

		// GET: api/<LogController>
		[HttpGet]
		public IEnumerable<LogModel> Get() {
			return logService.GetAll().ToList();
		}

		// GET api/<LogController>/5
		[HttpGet("{id}")]
		public string Get(int id) {
			return "";
		}

		// POST api/<LogController>
		[HttpPost]
		public void Post([FromBody] string value) {
			logService.Create(new LogModel() { Msg = "test"});
		}

		// PUT api/<LogController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value) {
		}

		// DELETE api/<LogController>/5
		[HttpDelete("{id}")]
		public void Delete(int id) {
		}
	}
}
