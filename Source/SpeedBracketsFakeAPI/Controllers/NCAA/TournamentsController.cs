using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SpeedBracketsFakeAPI.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SpeedBracketsFakeAPI.Controllers.NCAA
{
	[Route("api/v1/ncaa/[controller]")]
	public class TournamentsController : Controller
    {
		private readonly IHostingEnvironment environment;

		public List<Tournament> Tournaments { get; set; }

		public TournamentsController(IHostingEnvironment environment)
		{
			this.environment = environment;
			Tournaments = new List<Tournament>();
		}

		public void LoadTournaments()
		{
			LoadTournaments();

			string filePath = Path.Combine(environment.ContentRootPath, "AppData", "NCAA", "Tournaments");
			foreach (var file in Directory.GetFiles(filePath, "*.json"))
			{
				string jsonData = System.IO.File.ReadAllText(file);

				Tournaments.Add(JsonConvert.DeserializeObject<Tournament>(jsonData));
			}
		}

		[HttpGet("{tournamentId}/schedule.json")]
        public Tournament GetTournamentSchedule(string tournamentId)
		{
			return Tournaments.Where(x => x.id == tournamentId).FirstOrDefault();
		}
    }
}
