using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cde.Models.DTOs
{
	public class LogDTO
	{
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string Details { get; set; }
		public DateTime CreatedAt { get; set; }
		[Required]
		public string SystemName { get; set; }
		[Required]
		public string LevelName { get; set; }
	}
}
