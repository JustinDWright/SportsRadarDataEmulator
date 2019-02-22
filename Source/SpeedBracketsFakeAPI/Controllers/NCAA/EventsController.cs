using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SpeedBracketsFakeAPI.Services;
using System;
using System.IO;
using System.Linq;
using System.Text;
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

			var homeScore = 0;
			var awayScore = 0;

			foreach (var game in gameService.CurrentGames.OrderBy(g => g.scheduled))
			{
				for (var gameDelta = 0; gameDelta < game.periods.SelectMany(p => p.events).Count(); gameDelta++)			
				{
					var gameData = gameService.GetGameData(game.id, gameDelta);					

					if (gameData.payload.Event.statistics != null)
					{
						foreach(var stat in gameData.payload.Event.statistics.Where(x => x.points > 0))
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
	}
}
