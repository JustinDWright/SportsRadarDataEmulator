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
	public class SeasonsController : Controller
    {
		private readonly IHostingEnvironment environment;

		public List<Standings> SeasonStandings { get; set; }

		public SeasonsController(IHostingEnvironment environment)
		{
			this.environment = environment;

			LoadStandings();
		}

		public void LoadStandings()
		{
			SeasonStandings = new List<Standings>();

			string filePath = Path.Combine(environment.ContentRootPath, "AppData", "NCAA", "Standings");
			foreach (var file in Directory.GetFiles(filePath, "*.json"))
			{
				string jsonData = System.IO.File.ReadAllText(file);

				SeasonStandings.Add(JsonConvert.DeserializeObject<Standings>(jsonData));
			}
		}

		[HttpGet("{year}/{seasonType}/standings.json")]
		public Standings GetSeasonStandings(int year, string seasonType)
        {
            return SeasonStandings.Where(x => x.season.year == year && x.season.type.Equals(seasonType, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
		}
    }
}
