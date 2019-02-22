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

		public string FilePath { get; set; }
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

			FilePath = Path.Combine(environment.ContentRootPath, "AppData", "NCAA", "GameData");
			foreach (var file in Directory.GetFiles(FilePath, "*.json"))
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

		public bool GameDataExists(string gameId)
		{
			var filename = Path.Combine(FilePath, $"{gameId}.json");
			return File.Exists(filename);
		}

		public void SaveGame(string gameId, string gameData)
		{
			var filename = Path.Combine(FilePath, $"{gameId}.json");
			if (!GameDataExists(gameId))
			{
				File.WriteAllText(filename, gameData);
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
					.ToList();

			var currentEvent = events[gameDelta.Value];
			var period = game.periods.Where(x => x.events.Any(e => e.id == currentEvent.id)).FirstOrDefault();
			var eventGame = game.ToGameOnly();

			switch (currentEvent.event_type)
			{				
				case "endperiod":
					if (currentEvent.description == "End of 1st Half.")
					{
						eventGame.status = "halftime";
					}
					if (currentEvent.description == "End of 2nd Half.")
					{
						eventGame.status = "complete";
					}
					break;
				default:
					eventGame.status = "inprogress";
					break;
			}

			response.payload.game = eventGame;
			response.payload.Event = currentEvent;
			response.payload.Event.period = period.ToEventPeriod();

			return response;
		}		

		public GameStatistic GetGameStatistics(string gameId)
		{
			return Statistics.FirstOrDefault(x => x.id == gameId);
		}
	}
}
