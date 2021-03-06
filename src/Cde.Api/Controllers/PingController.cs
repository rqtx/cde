﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cde.Api;
using Cde.Api.Constants;
using Cde.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cde.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class PingController : ControllerBase
	{
		// GET: api/<PingController>
		[HttpGet]
		public ActionResult<string> Get() {
			return Ok("Pong");
		}

	}
}
