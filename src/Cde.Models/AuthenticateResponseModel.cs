using System;
using System.Collections.Generic;
using System.Text;

namespace Cde.Models
{
	public class AuthenticateResponseModel
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public AuthenticateResponseModel(UserModel user, string token) {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            Token = token;
        }
    }
}
