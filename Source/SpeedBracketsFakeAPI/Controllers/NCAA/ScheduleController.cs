using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SpeedBracketsFakeAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SpeedBracketsFakeAPI.Controllers.NCAA
{
	[Route("api/v1/ncaa/[controller]")]
	public class ScheduleController : Controller
	{
		private readonly IHostingEnvironment environment;

		public List<Schedule> Schedules { get; set; }

		public ScheduleController(IHostingEnvironment environment)
		{
			this.environment = environment;
			LoadSchedules();
		}

		public void LoadSchedules()
		{
			Schedules = new List<Schedule>();

			string filePath = Path.Combine(environment.ContentRootPath, "AppData", "NCAA", "Schedule");
			foreach (var file in Directory.GetFiles(filePath, "*.json"))
			{
				string jsonData = System.IO.File.ReadAllText(file);

				Schedules.Add(JsonConvert.DeserializeObject<Schedule>(jsonData));
			}
		}

		[HttpGet("{year}/{seasonType}/schedule.json")]
		public Schedule GetTournamentSchedule(int year, string seasonType)
		{
			return Schedules.Where(x => x.season.year == year && x.season.type.Equals(seasonType, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
		}
	}
}
