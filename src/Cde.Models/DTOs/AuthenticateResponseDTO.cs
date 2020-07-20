using System;
using System.Collections.Generic;
using System.Text;

namespace Cde.Models.DTOs
{
	public class AuthenticateResponseDTO
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }

        public AuthenticateResponseDTO(UserModel user, string token) {
            Id = user.Id;
            Name = user.Name;
            Token = token;
        }
    }
}
