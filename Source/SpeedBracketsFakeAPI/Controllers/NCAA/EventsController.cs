using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SpeedBracketsFakeAPI.Services;
using System;
using System.Collections.Concurrent;
using System.IO;
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

		[HttpGet("subscribea")]
		public async Task Get()
		{
			var response = httpContextAccessor.HttpContext.Response;

			for (var gameDelta = 0; true; gameDelta++)
			{
				foreach (var game in gameService.CurrentGames)
				{
					var gameData = JsonConvert.SerializeObject(gameService.GetGameData(game.Key, gameDelta));

					await response.WriteAsync(gameData, Encoding.UTF8);

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
							var gameData = JsonConvert.SerializeObject(gameService.GetGameData(game.Key, gameDelta));

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

		[HttpGet("subscribe")]
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
