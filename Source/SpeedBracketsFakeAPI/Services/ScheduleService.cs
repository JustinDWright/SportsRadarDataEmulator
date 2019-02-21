using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using SpeedBracketsFakeAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedBracketsFakeAPI.Services
{
	public class ScheduleService
	{
		private readonly IHostingEnvironment environment;

		public List<Schedule> Schedules { get; set; }

		public ScheduleService(IHostingEnvironment environment)
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

		public Schedule GetSchedule(int year, string seasonType)
		{
			return Schedules.Where(x => x.season.year == year && x.season.type.Equals(seasonType, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
		}
	}
}
