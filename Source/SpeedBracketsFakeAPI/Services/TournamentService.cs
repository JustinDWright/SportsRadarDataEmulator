using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using SpeedBracketsFakeAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SpeedBracketsFakeAPI.Services
{
	public class TournamentService
	{
		private readonly IHostingEnvironment environment;

		public List<Tournament> Tournaments { get; set; }

		public TournamentService(IHostingEnvironment environment)
		{
			this.environment = environment;
			LoadTournaments();
		}

		public void LoadTournaments()
		{
			Tournaments = new List<Tournament>();

			string filePath = Path.Combine(environment.ContentRootPath, "AppData", "NCAA", "Tournaments");
			foreach (var file in Directory.GetFiles(filePath, "*.json"))
			{
				string jsonData = File.ReadAllText(file);

				Tournaments.Add(JsonConvert.DeserializeObject<Tournament>(jsonData));
			}
		}

		public Tournament GetTournament(string tournamentId)
		{
			return Tournaments.Where(x => x.id == tournamentId).FirstOrDefault();
		}
	}
}
