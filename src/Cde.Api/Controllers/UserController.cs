using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cde.Database;
using Cde.Database.Services;
using Cde.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cde.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class UserController : ControllerBase
	{
		private readonly DatabaseService<UserModel> userService;

		public UserController(ApplicationDbContext context) {
			userService = new DatabaseService<UserModel>(context);
		}

		// GET: api/<UserController>
		[HttpGet]
		public ActionResult<List<UserDto>> GetAllUsers() {
			return Ok(userService.GetAll().Select(u => new UserDto() { 
				Id = u.Id,
				Name = u.Name,
				Email = u.Email
			}).ToList());
		}

		// GET api/<UserController>/5
		[HttpGet("{id}")]
		public ActionResult<UserDto> GetUser(int id) {
			try {
				return Ok(userService.Get(u => u.Id == id).Select(u => new UserDto() {
					Id = u.Id,
					Name = u.Name,
					Email = u.Email
				}).First());
			} catch (Exception e) {
				if (e is ArgumentNullException || e is InvalidOperationException) {
					return NotFound("User not found");
				}
				throw e;
			}
		}

		// POST api/<UserController>
		[HttpPost]
		public ActionResult Post([FromBody] UserForm userForm) {
			try {
				UserModel user = new UserModel() {
					Name = userForm.Name,
					Email = userForm.Email,
					CreatedAt = DateTime.UtcNow
				};
				using (SHA512Managed hashTool = new SHA512Managed()) {
					user.Salt = Convert.ToBase64String(hashTool.ComputeHash(System.Text.Encoding.UTF8.GetBytes(DateTime.Now + userForm.Password)));
					user.Passhash = Convert.ToBase64String(hashTool.ComputeHash(System.Text.Encoding.UTF8.GetBytes(user.Salt + userForm.Password)));
				}
				userService.Create(user);
				return Created("", new UserDto() { Id = user.Id, Name = user.Name, Email = user.Email});
			} catch (DbUpdateException) {
				return Conflict("User alredy exists!");
			}
		}

		// PUT api/<UserController>/5
		[HttpPut("{id}")]
		public ActionResult Put(int id, [FromBody] UserModel user) {
			try {
				var updatedUser = userService.Get(u => u.Id == id).First();
				updatedUser.Id = id;
				updatedUser.Name = user.Name;
				updatedUser.Email = user.Email;
				userService.Update(updatedUser);
				return Ok();
			} catch (Exception e) {
				if (e is ArgumentNullException || e is InvalidOperationException) {
					return NotFound("User not found");
				}
				throw e;
			}
		}

		// DELETE api/<UserController>/5
		[HttpDelete("{id}")]
		public ActionResult Delete(int id) {
			try {
				var user = userService.Get(u => u.Id == id).First();
				userService.Delete(user);
				return Ok();
			} catch (Exception e) {
				if (e is ArgumentNullException || e is InvalidOperationException) {
					return NotFound("User not found");
				}
				throw e;
			}
		}
	}
}
