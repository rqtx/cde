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
using Cde.Api.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Cde.Models.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cde.Controllers
{
	[Authorize]
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
		public ActionResult<List<UserModel>> GetAllUsers() {
			return Ok(userService.GetAll().ToList());
		}

		// GET api/<UserController>/5
		[HttpGet("{id}")]
		public ActionResult<UserModel> GetUser(int id) {
			try {
				return Ok(userService.Get(u => u.Id == id).First());
			} catch (Exception e) {
				if (e is ArgumentNullException || e is InvalidOperationException) {
					return NotFound(new { error = "User not found" });
				}
				throw e;
			}
		}

		// POST api/<UserController>
		[HttpPost]
		public ActionResult<UserModel> Post([FromBody] UserFormDTO userForm) {
			if (null != userService.Get(u => u.Email == userForm.Email).FirstOrDefault()) {
				return Conflict(new { error = "User alredy exist!" });
			}
			UserModel user = new UserModel() {
				Name = userForm.Name,
				Email = userForm.Email,
				CreatedAt = DateTime.UtcNow,
				Salt = PasswordManager.GenerateSalt(userForm.Email)
			};
			user.Passhash = PasswordManager.GeneratePasshash(user.Salt, userForm.Password);
			return Created("", userService.Create(user));
		}

		// PUT api/<UserController>/5
		[HttpPut("{id}")]
		public ActionResult<UserModel> Put(int id, [FromBody] UserModel user) {
			var updatedUser = userService.Get(u => u.Id == id).FirstOrDefault();
			if (null == updatedUser) {
				return NotFound(new { error = "User not found" });
			}
			updatedUser.Id = id;
			updatedUser.Name = user.Name;
			updatedUser.Email = user.Email;
			return Ok(userService.Update(updatedUser));
		}

		[HttpPut("password/{id}")]
		public ActionResult<UserModel> Put(int id, [FromBody] UserFormDTO userForm) {
			var updatedUser = userService.Get(u => u.Id == id).FirstOrDefault();
			if (null == updatedUser) {
				return NotFound(new { error = "User not found" });
			}
			updatedUser.Salt = PasswordManager.GenerateSalt(updatedUser.Email);
			updatedUser.Passhash = PasswordManager.GeneratePasshash(updatedUser.Salt, userForm.Password);
			return Ok(userService.Update(updatedUser));
		}

		// DELETE api/<UserController>/5
		[HttpDelete("{id}")]
		public ActionResult Delete(int id) {
			var user = userService.Get(u => u.Id == id).FirstOrDefault();
			if (null == user) {
				return NotFound(new { error = "User not found" });
			}
			userService.Delete(user);
			return Ok();
		}

		[AllowAnonymous]
		[HttpPost("authenticate")]
		public ActionResult<AuthenticateResponseDTO> Authenticate([FromBody] AuthenticateRequestDTO model) {
			var dbUser = userService.Get(u => u.Email == model.Email).FirstOrDefault();
			if (null == dbUser) {
				return BadRequest(new { error = "User does not exist" });
			} else if (PasswordManager.GeneratePasshash(dbUser.Salt, model.Password) != dbUser.Passhash) {
				return BadRequest(new { error = "Email or password is incorrect" });
			}
			var userToken = TokenProvider.GenerateToken(dbUser);
			
			return Ok(new AuthenticateResponseDTO(dbUser, userToken));
		}
	}
}
