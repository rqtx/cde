using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Cde.Api.Helpers
{
	public static class PasswordManager
	{
		static public string GenerateSalt(string seed) {
			using (SHA512Managed hashTool = new SHA512Managed()) {
				return Convert.ToBase64String(hashTool.ComputeHash(System.Text.Encoding.UTF8.GetBytes(DateTime.Now + seed)));
			}
		}

		static public string GeneratePasshash(string salt, string password) {
			using (SHA512Managed hashTool = new SHA512Managed()) {
				return Convert.ToBase64String(hashTool.ComputeHash(System.Text.Encoding.UTF8.GetBytes(salt + password)));
			}
		}
	}
}
