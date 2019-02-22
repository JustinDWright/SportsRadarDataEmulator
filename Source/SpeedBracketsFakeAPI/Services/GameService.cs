using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using SpeedBracketsFakeAPI.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SpeedBracketsFakeAPI.Services
{
	public class GameService
	{		
		private readonly IHostingEnvironment environment;

		public string GameFilePath { get; set; }		
		public string BoxScoreFilePath { get; set; }
		public string SummariesPath { get; set; }
		public string StatisticsFilePath { get; set; }
		public List<Game> CurrentGames { get; set; }
		public List<BoxScore> BoxScores { get; set; }
		public List<GameSummary> Summaries { get; set; }
		public List<GameStatistic> Statistics { get; set; }

		public GameService(IHostingEnvironment environment)
		{
			this.environment = environment;
			LoadGames();
			LoadBoxScores();
			LoadGameSummaries();
			LoadStatistics();
		}

		private void LoadGames()
		{
			CurrentGames = new List<Game>();

			GameFilePath = Path.Combine(environment.ContentRootPath, "AppData", "NCAA", "GameData");
			foreach (var file in Directory.GetFiles(GameFilePath, "*.json"))
			{
				string jsonData = File.ReadAllText(file);

				CurrentGames.Add(JsonConvert.DeserializeObject<Game>(jsonData));
			}
		}

		public void LoadBoxScores()
		{
			BoxScores = new List<BoxScore>();

			BoxScoreFilePath = Path.Combine(environment.ContentRootPath, "AppData", "NCAA", "BoxScores");
			foreach (var file in Directory.GetFiles(BoxScoreFilePath, "*.json"))
			{
				string jsonData = File.ReadAllText(file);

				BoxScores.Add(JsonConvert.DeserializeObject<BoxScore>(jsonData));
			}
		}

		private void LoadGameSummaries()
		{
			Summaries = new List<GameSummary>();

			SummariesPath = Path.Combine(environment.ContentRootPath, "AppData", "NCAA", "Summaries");
			foreach (var file in Directory.GetFiles(SummariesPath, "*.json"))
			{
				string jsonData = File.ReadAllText(file);

				Summaries.Add(JsonConvert.DeserializeObject<GameSummary>(jsonData));
			}
		}

		private void LoadStatistics()
		{
			Statistics = new List<GameStatistic>();

			StatisticsFilePath = Path.Combine(environment.ContentRootPath, "AppData", "NCAA", "Statistics");
			foreach (var file in Directory.GetFiles(StatisticsFilePath, "*.json"))
			{
				string jsonData = File.ReadAllText(file);

				Statistics.Add(JsonConvert.DeserializeObject<GameStatistic>(jsonData));
			}
		}		

		public bool GameDataExists(string gameId)
		{
			var filename = Path.Combine(GameFilePath, $"{gameId}.json");
			return File.Exists(filename);
		}

		public void SaveGame(string gameId, string gameData)
		{
			var filename = Path.Combine(GameFilePath, $"{gameId}.json");
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

		public bool BoxScoreExists(string gameId)
		{
			var filename = Path.Combine(BoxScoreFilePath, $"{gameId}.json");
			return File.Exists(filename);
		}

		public void SaveBoxScore(string gameId, string boxScore)
		{
			var filename = Path.Combine(BoxScoreFilePath, $"{gameId}.json");
			if (!BoxScoreExists(gameId))
			{
				File.WriteAllText(filename, boxScore);
			}
		}

		public BoxScore GetBoxScore(string gameId)
		{
			return BoxScores.FirstOrDefault(x => x.id == gameId);
		}

		public GameStatistic GetGameStatistics(string gameId)
		{
			return Statistics.FirstOrDefault(x => x.id == gameId);
		}

		public bool SummaryExists(string gameId)
		{
			var filename = Path.Combine(SummariesPath, $"{gameId}.json");
			return File.Exists(filename);
		}

		public void SaveSummary(string gameId, string summary)
		{
			var filename = Path.Combine(SummariesPath, $"{gameId}.json");
			if (!SummaryExists(gameId))
			{
				File.WriteAllText(filename, summary);
			}
		}

		public GameSummary GetGameSummary(string gameId)
		{
			return Summaries.FirstOrDefault(x => x.id == gameId);
		}
	}
}
