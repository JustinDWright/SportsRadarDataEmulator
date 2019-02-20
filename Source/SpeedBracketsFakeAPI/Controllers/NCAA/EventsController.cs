using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SpeedBracketsFakeAPI.Services;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SpeedBracketsFakeAPI.Controllers.NCAA
{
	[Route("api/v1/ncaa/[controller]")]
	public class EventsController : ControllerBase
	{
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly GameService gameService;

		private static ConcurrentBag<StreamWriter> clients;
		private static Timer timer;

		public EventsController(IHttpContextAccessor httpContextAccessor, GameService gameService)
		{
			this.httpContextAccessor = httpContextAccessor;
			this.gameService = gameService;

			clients = new ConcurrentBag<StreamWriter>();

			timer = new Timer();
			timer.Interval = 5000;
			timer.AutoReset = true;
			timer.Elapsed += timer_Elapsed;
			timer.Start();			
		}

		[HttpGet("subscribe")]
		public async Task Get()
		{
			var response = httpContextAccessor.HttpContext.Response;

			var homeScore = 0;
			var awayScore = 0;

			for (var gameDelta = 0; true; gameDelta++)
			{
				foreach (var game in gameService.CurrentGames)
				{
					if (gameDelta >= game.periods.SelectMany(p => p.events).Count())
					{
						return;
					}

					var gameData = gameService.GetGameData(game.id, gameDelta);

					switch (gameData.payload.Event.event_type)
					{
						case "opentip":
							gameData.payload.game.status = "inprogress";
							break;
						case "half":
							gameData.payload.game.status = "halftime";
							break;
						case "endperiod":
							if (gameData.payload.Event.description == "End of 2nd Half.")
							{
								gameData.payload.game.status = "complete";
							}
							break;
					}

					var statistics = gameData.payload.Event.statistics;

					if (statistics != null)
					{
						foreach(var stat in statistics.Where(x => x.points > 0))
						{
							if (stat.team.id == gameData.payload.game.home.id)
							{
								homeScore += stat.points;
							}
							else
							{
								awayScore += stat.points;
							}
						}
					}

					gameData.payload.game.home.points = homeScore;
					gameData.payload.game.away.points = awayScore;
					gameData.payload.game.clock = gameData.payload.Event.clock;

					var serializedGameData = JsonConvert.SerializeObject(gameData);
					serializedGameData += Environment.NewLine;
					
					await response.WriteAsync(serializedGameData, Encoding.UTF8);

					response.Body.Flush();
					
					await Task.Delay(5 * 1000);
				}
			}
		}

		private async void timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			foreach (var client in clients)
			{
				try
				{
					for (var gameDelta = 0; true; gameDelta++)
					{
						foreach (var game in gameService.CurrentGames)
						{
							var gameData = JsonConvert.SerializeObject(gameService.GetGameData(game.id, gameDelta));

							await client.WriteAsync(gameData);
							await client.FlushAsync();
						}
					}					
				}
				catch (Exception ex)
				{
					StreamWriter ignore;
					clients.TryTake(out ignore);
				}
			}
		}

		[HttpGet("subscribe_v2")]
		public HttpResponseMessage Subscribe(HttpRequestMessage request)
		{
			var response = request.CreateResponse();
			response.Content = new PushStreamContent((a, b, c) =>
			{ OnStreamAvailable(a, b, c); }, "text/event-stream");
			return response;
		}

		private void OnStreamAvailable(Stream stream, HttpContent content, TransportContext context)
		{
			var client = new StreamWriter(stream);
			clients.Add(client);
		}
	}
}
