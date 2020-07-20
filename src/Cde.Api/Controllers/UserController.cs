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
using Cde.Database.IServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cde.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly IRoleService _roleService;
		private readonly IMapper _mapper;

		public UserController(IUserService userService, IRoleService roleService, IMapper mapper) {
			_userService = userService;
			_roleService = roleService;
			_mapper = mapper;
		}

		/**
		 * <summary> GET method that return all users </summary>
		 * <example>api/user</example>
		 * <returns>List of UserDTO</returns>
		 * **/
		// GET api/<UserController>
		[HttpGet]
		[Authorize(Roles = Roles.Admin)]
		public ActionResult<IEnumerable<UserDTO>> GetAllUsers() {
			return Ok(_mapper.Map<List<UserDTO>>(_userService.GetAll()));
		}

		/**
		 * <summary> GET method that return a user by id </summary>
		 * <example>api/user/1</example>
		 * <returns>UserDTO</returns>
		 * **/
		// GET api/<UserController>/5
		[HttpGet("{id}")]
		public ActionResult<UserDTO> GetUser(int id) {
			var user = _userService.Get(u => u.Id == id).FirstOrDefault();
			if (null == user) {
				return NotFound(new { error = "User not found" });
			}
			if (user.Name != User.FindFirst(ClaimTypes.Name).Value && !UserHelper.IsAdim(User)) {
				return BadRequest("Your role cannot see other user data");
			}
			return Ok(_mapper.Map<UserDTO>(user));
		}

		/**
		 * <summary> POST method to create a user </summary>
		 * <example>api/user</example>
		 * <param name="userForm"> UserFormDTO recived from body as json</param>
		 * <returns>Created user as UserDTO</returns>
		 * **/
		// POST api/<UserController>
		[HttpPost]
		[Authorize(Roles = Roles.Admin)]
		public ActionResult<UserDTO> Post([FromBody] UserFormDTO userForm) {
			if (null != _userService.Get(u => u.Name == userForm.Name).FirstOrDefault()) {
				return Conflict(new { error = "User alredy exist!" });
			}
			var role = _roleService.Get(r => r.Name == userForm.Role.ToLower()).FirstOrDefault();
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
			return Created("", _mapper.Map <UserDTO>(_userService.Create(user)));
		}

		// PUT api/<UserController>/5
		[HttpPut("role")]
		[Authorize(Roles = Roles.Admin)]
		public ActionResult<UserDTO> PutUpdateRole([FromBody] UserDTO user) {
			var updatedUser = _userService.Get(u => u.Name == user.Name).FirstOrDefault();
			if (null == updatedUser) {
				return NotFound(new { error = "User not found" });
			}
			var role = _roleService.Get(r => r.Name == user.RoleName.ToLower()).FirstOrDefault();
			if (null == role) {
				return BadRequest(new { error = "Role does not exist" });
			}
			updatedUser.RoleId = role.Id;
			return Ok(_mapper.Map<UserDTO>(_userService.Update(updatedUser)));
		}

		[HttpPut("password/{id}")]
		public ActionResult<UserDTO> PutUpdatePass(int id, [FromBody] AuthenticateRequestDTO user) {
			var updatedUser = _userService.Get(u => u.Id == id).FirstOrDefault();
			if (null == updatedUser) {
				return NotFound(new { error = "User not found" });
			}
			if (updatedUser.Name != User.FindFirst(ClaimTypes.Name).Value) {
				return BadRequest(new { error = "Cannot update password from another user" });
			}
			updatedUser.Salt = PasswordManager.GenerateSalt(updatedUser.Name);
			updatedUser.Passhash = PasswordManager.GeneratePasshash(updatedUser.Salt, user.Password);
			return Ok(_mapper.Map<UserDTO>(_userService.Update(updatedUser)));
		}

		// DELETE api/<UserController>/5
		[HttpDelete("{id}")]
		public ActionResult Delete(int id) {
			var user = _userService.Get(u => u.Id == id).FirstOrDefault();
			if (null == user) {
				return NotFound(new { error = "User not found" });
			}
			if (user.Name != User.FindFirst(ClaimTypes.Name).Value && !UserHelper.IsAdim(User)) {
				return BadRequest(new { error = "Cannot delete user" });
			}
			_userService.Delete(user);
			return Ok();
		}

		[AllowAnonymous]
		[HttpPost("authenticate")]
		public ActionResult<AuthenticateResponseDTO> Authenticate([FromBody] AuthenticateRequestDTO model) {
			var dbUser = _userService.Get(u => u.Name == model.Name).FirstOrDefault();
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
