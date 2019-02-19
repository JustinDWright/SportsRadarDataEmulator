using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SpeedBracketsFakeAPI.Services;
using System.Threading.Tasks;

namespace SpeedBracketsFakeAPI.Controllers.NCAA
{
	[Route("api/v1/ncaa/[controller]")]
	public class EventsController : ControllerBase
	{
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly GameService gameService;

		public EventsController(IHttpContextAccessor httpContextAccessor, GameService gameService)
		{
			this.httpContextAccessor = httpContextAccessor;
			this.gameService = gameService;
		}

		[HttpGet("subscribe")]
		public async Task Get()
		{
			var response = httpContextAccessor.HttpContext.Response;
			response.Headers.Add("Content-Type", "text/event-stream");
			response.StatusCode = 200;

			for (var gameDelta = 0; true; gameDelta++)
			{
				foreach (var game in gameService.CurrentGames)
				{
					var gameData = JsonConvert.SerializeObject(gameService.GetGameData(game.Key, gameDelta));

					await response.WriteAsync(gameData);

					response.Body.Flush();
					await Task.Delay(5 * 1000);
				}
			}
		}
	}
}
