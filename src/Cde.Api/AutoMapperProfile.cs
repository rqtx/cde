using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cde.Models;
using Cde.Models.DTOs;

namespace Cde.Api
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile() {
			CreateMap<LogModel, LogDTO>().ReverseMap();
		}
	}
}
