using Microsoft.AspNetCore.Mvc;
using SpeedBracketsFakeAPI.Models;
using SpeedBracketsFakeAPI.Services;
using System;

namespace SpeedBracketsFakeAPI.Controllers.NCAA
{
	[Route("api/v1/ncaa/[controller]")]
	public class GamesController : ControllerBase
	{
		private readonly GameService service;

		public GamesController(GameService service)
		{
			this.service = service;			
		}		
		
		[HttpGet("{gameId}/pbp")]
		public RealTimeEvent Pbp(string gameId)
		{
			return service.GetGameData(gameId);
		}

		[HttpGet("{gameId}/pbpEvent/{gameDelta?}")]
		public RealTimeEvent PbpEvent(string gameId, int gameDelta = 1)
		{
			return service.GetGameData(gameId, gameDelta);
		}

		[HttpGet("{gameId}/statistics.json")]
		public GameStatistic GetGameStatistics(string gameId)
		{
			return service.GetGameStatistics(gameId);
		}
	}
}
