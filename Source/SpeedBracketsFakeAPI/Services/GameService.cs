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

		public Dictionary<Guid, Game> CurrentGames { get; set; }

		public GameService(IHostingEnvironment environment)
		{
			this.environment = environment;
			CurrentGames = new Dictionary<Guid, Game>();
			LoadGames();
		}

		public RealTimeEvent GetGameData(Guid gameId, int? gameDelta = null)
		{
			var game = CurrentGames.FirstOrDefault(x => x.Key == gameId).Value;

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

			response.payload.game = game.ToGameOnly();			
			response.payload._event = events[gameDelta.Value];

			return response;
		}

		private void LoadGames()
		{			
			string filePath = Path.Combine(environment.ContentRootPath, "AppData", "NCAA", "GameData");
			foreach (var file in Directory.GetFiles(filePath, "*.json"))
			{
				string jsonData = File.ReadAllText(file);

				CurrentGames.Add(Guid.Parse(Path.GetFileNameWithoutExtension(file)), JsonConvert.DeserializeObject<Game>(jsonData));
			}
		}

		private DateTime GetPlayClock(int gameLengthinSeconds, DateTime gameStart)
		{
			// The modulus of the number of minutes since midnight and the game length
			int secondsSinceMidnight = (int)(DateTime.Now - DateTime.Today).TotalSeconds;
			int secondsIntoGame = secondsSinceMidnight % gameLengthinSeconds;
			return gameStart.AddSeconds(secondsIntoGame);
		}

		private int GetPeriodCount(Game result)
		{
			return result.periods
				.OrderByDescending(p => p.sequence)
				.Select(p => p.number)
				.FirstOrDefault();
		}

		public int GetPeriodLength(Game result)
		{
			var clock = result.periods
					.Where(n => n.events != null)
					.SelectMany(n => n.events)
					.OrderByDescending(n => n.updated)
					.Select(n => n.clock)
					.FirstOrDefault();

			var duration = TimeSpan.Parse($"00:{clock}");
			return duration.Minutes;
		}

		private int GetGameLength(Game result)
		{
			var periods = GetPeriodCount(result);
			var periodLength = GetPeriodLength(result);
			
			return periods * periodLength;
		}

		//private static void RemoveFutureEvents(DateTime now, GameEvent result)
		//{
		//	var periodRemoveList = new List<Period>();
		//	for (int periodsIdx = 0; periodsIdx < result.periods.Count; periodsIdx++)
		//	{
		//		var eventRemoveList = new List<Event>();
		//		for (int eventIdx = 0; eventIdx < result.periods[periodsIdx].events.Count; eventIdx++)
		//		{
		//			// If there is no time, it's a special kind of event {timeout, game start, game over, etc)
		//			// We keep these if there is an event after it that is keepable
		//			if (eventIdx + 1 < result.periods[periodsIdx].events.Count)
		//			{
		//				DateTime evntDt = result.periods[periodsIdx].events[eventIdx + 1].updated;
		//				if (evntDt == DateTime.MinValue || evntDt > now)
		//					eventRemoveList.Add(result.periods[periodsIdx].events[eventIdx]);
		//			}
		//			else
		//			{
		//				// Remove it anyway
		//				eventRemoveList.Add(result.periods[periodsIdx].events[eventIdx]);
		//			}

		//			if (result.periods[periodsIdx].events[eventIdx].updated > now)
		//				eventRemoveList.Add(result.periods[periodsIdx].events[eventIdx]);
		//		}

		//		eventRemoveList.ForEach(n => result.periods[periodsIdx].events.Remove(n));

		//		if (result.periods[periodsIdx].events.Count == 0)
		//			periodRemoveList.Add(result.periods[periodsIdx]);
		//	}

		//	periodRemoveList.ForEach(n => result.periods.Remove(n));			
		//}

		private static void FixupGameSummary(Game result, int gameDelta)
		{
			// Fix the game clock
			FixGameClock(result, gameDelta);

			// Fix the status -- TOOD: what should the status be?
			result.status = "inprogress";

			// scheduled
			// created
			// inprogress
			// halftime
			// complete
			// closed
			// canceled
			// delayed
			// postponed
			// time-tbd
			// unnecessary
		}

		private static void FixGameClock(Game result, int gameDelta)
		{
			var evnt = result.periods
						.Where(n => n.events != null)
						.SelectMany(n => n.events)
						.OrderByDescending(n => n.updated)
						.FirstOrDefault();

			var currentTimeClock = TimeSpan.Parse($"00:{result.clock}").Add(TimeSpan.FromMinutes(gameDelta));
			result.clock = currentTimeClock.ToString("mm:ss");
		}
	}
}
