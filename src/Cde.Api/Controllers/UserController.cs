using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cde.Database;
using Cde.Database.Services;
using Cde.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cde.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly UserService userService;

		public UserController(ApplicationContext context) {
			userService = new UserService(context);
		}

		// GET: api/<UserController>
		[HttpGet]
		public ActionResult<IEnumerable<IUser>> GetAllUsers() {
			return userService.GetAll().ToList();
		}

		// GET api/<UserController>/5
		[HttpGet("{id}")]
		public ActionResult<IUser> GetUser(int id) {
			var user = userService.GetById(id);
			if (null == user) {
				return NotFound("User not found");
			}
			return user;
		}

		// POST api/<UserController>
		[HttpPost]
		public ActionResult<IUser> Post([FromForm] UserForm userForm) {
			UserModel user = new UserModel() {
				Name = userForm.Name,
				Email = userForm.Email,
			};
			using (SHA512Managed hashTool = new SHA512Managed()) {
				user.Salt = Convert.ToBase64String(hashTool.ComputeHash(System.Text.Encoding.UTF8.GetBytes(DateTime.Now + userForm.Password)));
				user.Passhash = Convert.ToBase64String(hashTool.ComputeHash(System.Text.Encoding.UTF8.GetBytes(user.Salt + userForm.Password)));
			}
			userService.Create(user);
			var savedUser = userService.GetUserByEmail(user.Email);
			return savedUser;	
		}

		// PUT api/<UserController>/5
		[HttpPut("{id}")]
		public ActionResult<IUser> Put(int id, [FromForm] UserModel user) {
			userService.Update(id, user);
			var updatedUser = userService.GetUserByEmail(user.Email);
			return updatedUser;
		}

		// DELETE api/<UserController>/5
		[HttpDelete("{id}")]
		public ActionResult Delete(int id) {
			userService.Delete(id);
			return Ok();
		}
	}
}
