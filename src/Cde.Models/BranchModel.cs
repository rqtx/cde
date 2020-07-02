using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cde.Models
{
	public enum Branch 
	{
		Development,
		Homologation,
		Production
	}
	public class BranchModel
	{
		public int Id { get; set; }
		public Branch Branch { get; set; }
	}
}
