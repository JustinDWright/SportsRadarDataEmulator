using Newtonsoft.Json;
using SpeedBracketsFakeAPI.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SpeedBracketsFakeAPI.api.v1.Controllers
{
	public class PbpController : ApiController
	{
		private static Rootobject GameData;
		private Rootobject GetData()
		{
			string filePath = Path.Combine(HttpContext.Current.Server.MapPath("/"), "App_Data", "GameData.json");
			string jsonData = File.ReadAllText(filePath);
			return JsonConvert.DeserializeObject<Rootobject>(jsonData);
		}

		public PbpController()
		{
		}

		// GET: GamePlay
		public Rootobject GetGamePlay(int? gameDelta = null)
		{
			var result = GetData();

			DateTime now;
			if (gameDelta != null)
				now = DateTime.Now.AddMinutes(gameDelta.Value * -1);
			else
				now = GetPlayClock(GetGameLength(result), result.scheduled);


			// Cull out anything that doesn't belong
			RemoveFutureEvents(now, result);


			return result;
		}

		private DateTime GetPlayClock(int gameLengthinSeconds, DateTime gameStart)
		{
			// The modulus of the number of minutes since midnight and the game length
			int secondsSinceMidnight = (int)(DateTime.Now - DateTime.Today).TotalSeconds;
			int secondsIntoGame = secondsSinceMidnight % gameLengthinSeconds;
			return gameStart.AddSeconds(secondsIntoGame);
		}

		private int GetGameLength(Rootobject result)
		{
			DateTime start = result.scheduled;
			DateTime end = result.periods
				.Where(n => n.pbp != null)
				.SelectMany(n => n.pbp)
				.Where(n => n.events != null)
				.SelectMany(n => n.events)
				.Where(n => n.wall_clock != DateTime.MinValue)
				.OrderByDescending(n => n.wall_clock)
				.Select(n => n.wall_clock)
				.FirstOrDefault();
			TimeSpan duration = end - start;
			return (int)duration.TotalSeconds + (10 * 60);
		}

		private static void RemoveFutureEvents(DateTime now, Rootobject result)
		{
			var periodRemoveList = new List<Period>();
			for (int periodsIdx = 0; periodsIdx < result.periods.Count; periodsIdx++)
			{
				var pbpRemoveList = new List<Pbp>();
				for (int pbpIdx = 0; pbpIdx < result.periods[periodsIdx].pbp.Count; pbpIdx++)
				{
					if (result.periods[periodsIdx].pbp[pbpIdx].events != null)
					{
						var evntRemoveList = new List<Event>();
						for (int evntIdx = 0; evntIdx < result.periods[periodsIdx].pbp[pbpIdx].events.Count; evntIdx++)
						{
							// If there is no time, it's a special kind of event {timeout, game start, game over, etc)
							// We keep these if there is an event after it that is keepable
							if (evntIdx + 1 < result.periods[periodsIdx].pbp[pbpIdx].events.Count)
							{
								DateTime evntDt = result.periods[periodsIdx].pbp[pbpIdx].events[evntIdx + 1].wall_clock;
								if (evntDt == DateTime.MinValue || evntDt > now)
									evntRemoveList.Add(result.periods[periodsIdx].pbp[pbpIdx].events[evntIdx]);
							}
							else
							{
								// Remove it anyway
								evntRemoveList.Add(result.periods[periodsIdx].pbp[pbpIdx].events[evntIdx]);
							}

							if (result.periods[periodsIdx].pbp[pbpIdx].events[evntIdx].wall_clock > now)
								evntRemoveList.Add(result.periods[periodsIdx].pbp[pbpIdx].events[evntIdx]);
						}
						evntRemoveList.ForEach(n => result.periods[periodsIdx].pbp[pbpIdx].events.Remove(n));

						if (result.periods[periodsIdx].pbp[pbpIdx].events.Count == 0)
							pbpRemoveList.Add(result.periods[periodsIdx].pbp[pbpIdx]);
					}

				}
				pbpRemoveList.ForEach(n => result.periods[periodsIdx].pbp.Remove(n));

				if (result.periods[periodsIdx].pbp.Count == 0)
					periodRemoveList.Add(result.periods[periodsIdx]);
			}
			periodRemoveList.ForEach(n => result.periods.Remove(n));

			FixupGameSummary(result);
		}

		private static void FixupGameSummary(Rootobject result)
		{
			// Fix the score by looking for the last "score" element and setting the main score to that
			// Extra points look different
			var res = FindLastScore(result);
			if (res == null)
			{
				result.summary.home.points = 0;
				result.summary.away.points = 0;
			}
			else
			{
				result.summary.home.points = res.home_points;
				result.summary.away.points = res.away_points;
			}

			// Fix the quarter
			result.quarter = result.periods.Count;

			// Fix the game clock
			FixGameClock(result);

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

		private static void FixGameClock(Rootobject result)
		{
			var evnt = result.periods
					.Where(n => n.pbp != null)
					.SelectMany(n => n.pbp)
					.Where(n => n.events != null)
					.SelectMany(n => n.events)
					.OrderByDescending(n => n.sequence)
					.FirstOrDefault();

			result.clock = evnt.clock;
		}

		private static Score FindLastScore(Rootobject result)
		{
			if (result != null)
			{
				// Get all events (if any)
				var events = result.periods
					.Where(n => n.pbp != null)
					.SelectMany(n => n.pbp)
					.Where(n => n.events != null)
					.SelectMany(n => n.events)
					.Where(n => n.score != null)
					.Select(n => n.score)
					.OfType<Score>()
					.OrderByDescending(n => n.sequence);

				return events.FirstOrDefault();
			}

			return null;
		}
	}
}
