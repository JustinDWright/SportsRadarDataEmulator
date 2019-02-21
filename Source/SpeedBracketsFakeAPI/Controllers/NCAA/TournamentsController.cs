using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SpeedBracketsFakeAPI.Models;
using SpeedBracketsFakeAPI.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SpeedBracketsFakeAPI.Controllers.NCAA
{
	[Route("api/v1/ncaa/[controller]")]
	public class TournamentsController : Controller
    {
		private readonly IHostingEnvironment environment;
		private readonly ScheduleService scheduleService;

		public List<Tournament> Tournaments { get; set; }

		public TournamentsController(IHostingEnvironment environment, ScheduleService scheduleService)
		{
			this.environment = environment;
			this.scheduleService = scheduleService;

			LoadTournaments();
		}

		public void LoadTournaments()
		{
			Tournaments = new List<Tournament>();

			string filePath = Path.Combine(environment.ContentRootPath, "AppData", "NCAA", "Tournaments");
			foreach (var file in Directory.GetFiles(filePath, "*.json"))
			{
				string jsonData = System.IO.File.ReadAllText(file);

				Tournaments.Add(JsonConvert.DeserializeObject<Tournament>(jsonData));
			}
		}

		[HttpGet("{year}/{seasonType}/schedule.json")]
		public Schedule GetTournamentSchedule(int year, string seasonType)
		{
			return scheduleService.GetSchedule(year, seasonType);
		}

		[HttpGet("{tournamentId}/schedule.json")]
        public Tournament GetScheduleByTournamentId(string tournamentId)
		{
			return Tournaments.Where(x => x.id == tournamentId).FirstOrDefault();
		}
    }
}
