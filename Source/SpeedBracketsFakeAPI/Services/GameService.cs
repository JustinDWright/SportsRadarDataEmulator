using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using SpeedBracketsFakeAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SpeedBracketsFakeAPI.Services
{
	public class GameService
	{
		private readonly IHostingEnvironment environment;

		public List<Game> CurrentGames { get; set; }
		public List<GameStatistic> Statistics { get; set; }

		public GameService(IHostingEnvironment environment)
		{
			this.environment = environment;
			LoadGames();
			LoadStatistics();
		}

		private void LoadGames()
		{
			CurrentGames = new List<Game>();

			string filePath = Path.Combine(environment.ContentRootPath, "AppData", "NCAA", "GameData");
			foreach (var file in Directory.GetFiles(filePath, "*.json"))
			{
				string jsonData = File.ReadAllText(file);

				CurrentGames.Add(JsonConvert.DeserializeObject<Game>(jsonData));
			}
		}

		private void LoadStatistics()
		{
			Statistics = new List<GameStatistic>();

			string filePath = Path.Combine(environment.ContentRootPath, "AppData", "NCAA", "Statistics");
			foreach (var file in Directory.GetFiles(filePath, "*.json"))
			{
				string jsonData = File.ReadAllText(file);

				Statistics.Add(JsonConvert.DeserializeObject<GameStatistic>(jsonData));
			}
		}

		public RealTimeEvent GetGameData(string gameId, int? gameDelta = null)
		{
			var game = CurrentGames.FirstOrDefault(x => x.id == gameId);

			var response = new RealTimeEvent { payload = new RealTimeEventPayload { game = game }, locale = "en" };

			if (gameDelta == null)
			{
				return response;
			}

			var events = game.periods
					.Where(n => n.events != null)
					.SelectMany(n => n.events)
					.OrderBy(n => n.updated)
					.ToList();

			var current = events[gameDelta.Value];
			var period = game.periods.Where(x => x.events.Any(e => e.id == current.id)).FirstOrDefault();
			
			response.payload.game = game.ToGameOnly();			
			response.payload.Event = events[gameDelta.Value];
			response.payload.Event.period = period.ToEventPeriod();

			return response;
		}		

		public GameStatistic GetGameStatistics(string gameId)
		{
			return Statistics.FirstOrDefault(x => x.id == gameId);
		}
	}
}
