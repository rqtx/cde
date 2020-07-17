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
using System.Security.Claims;
using Cde.Api.Constants;

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
		private readonly DatabaseService<RoleModel> roleService;
		private readonly IMapper _mapper;

		public UserController(ApplicationDbContext context, IMapper mapper) {
			userService = new UserService(context);
			roleService = new DatabaseService<RoleModel>(context);
			_mapper = mapper;
		}

		// GET: api/<UserController>
		[HttpGet]
		[Authorize(Roles.Admin)]
		public ActionResult<List<UserDTO>> GetAllUsers() {
			return Ok(_mapper.Map<List<UserDTO>>(userService.GetAll().ToList()));
		}

		// GET api/<UserController>/5
		[HttpGet("{id}")]
		public ActionResult<UserDTO> GetUser(int id) {
			try {
				return Ok(_mapper.Map<UserDTO>(userService.Get(u => u.Id == id).First()));
			} catch (Exception e) {
				if (e is ArgumentNullException || e is InvalidOperationException) {
					return NotFound(new { error = "User not found" });
				}
				throw e;
			}
		}

		// POST api/<UserController>
		[HttpPost]
		[Authorize(Roles.Admin)]
		public ActionResult<UserModel> Post([FromBody] UserFormDTO userForm) {
			if (null != userService.Get(u => u.Name == userForm.Name).FirstOrDefault()) {
				return Conflict(new { error = "User alredy exist!" });
			}
			var role = roleService.Get(r => string.Equals(r.Name, userForm.Role, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
			if (null == role) {
				return BadRequest(new { error = "Role does not exist" });
			}
			UserModel user = new UserModel() {
				Name = userForm.Name,
				RoleId = role.Id,
				CreatedAt = DateTime.UtcNow,
				Salt = PasswordManager.GenerateSalt(userForm.Name)
			};
			user.Passhash = PasswordManager.GeneratePasshash(user.Salt, userForm.Password);
			return Created("", userService.Create(user));
		}

		// PUT api/<UserController>/5
		[HttpPut("role/{id}")]
		[Authorize(Roles.Admin)]
		public ActionResult<UserModel> PutUpdateRole(int id, [FromBody] string roleName) {
			var updatedUser = userService.Get(u => u.Id == id).FirstOrDefault();
			if (null == updatedUser) {
				return NotFound(new { error = "User not found" });
			}
			var role = roleService.Get(r => string.Equals(r.Name, roleName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
			if (null == role) {
				return BadRequest(new { error = "Role does not exist" });
			}
			updatedUser.RoleId = role.Id;
			return Ok(userService.Update(updatedUser));
		}

		[HttpPut("password/{id}")]
		public ActionResult<UserModel> PutUpdatePass(int id, [FromBody] string password) {
			var updatedUser = userService.Get(u => u.Id == id).FirstOrDefault();
			if (null == updatedUser) {
				return NotFound(new { error = "User not found" });
			}
			if (updatedUser.Name != User.FindFirst(ClaimTypes.Name).Value) {
				return BadRequest(new { error = "Cannot update password from another user" });
			}
			updatedUser.Salt = PasswordManager.GenerateSalt(updatedUser.Name);
			updatedUser.Passhash = PasswordManager.GeneratePasshash(updatedUser.Salt, password);
			return Ok(userService.Update(updatedUser));
		}

		// DELETE api/<UserController>/5
		[HttpDelete("{id}")]
		public ActionResult Delete(int id) {
			var user = userService.Get(u => u.Id == id).FirstOrDefault();
			if (null == user) {
				return NotFound(new { error = "User not found" });
			}
			if (user.Name != User.FindFirst(ClaimTypes.Name).Value || !UserHelper.IsAdim(User)) {
				return BadRequest(new { error = "Cannot delete user" });
			}
			userService.Delete(user);
			return Ok();
		}

		[AllowAnonymous]
		[HttpPost("authenticate")]
		public ActionResult<AuthenticateResponseDTO> Authenticate([FromBody] AuthenticateRequestDTO model) {
			var dbUser = userService.Get(u => u.Name == model.Name).FirstOrDefault();
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
