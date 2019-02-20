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
		public RealTimeEvent Pbp(Guid gameId)
		{
			return service.GetGameData(gameId);
		}

		[HttpGet("{gameId}/pbpEvent/{gameDelta?}")]
		public RealTimeEvent PbpEvent(Guid gameId, int gameDelta = 1)
		{
			return service.GetGameData(gameId, gameDelta);
		}		
	}
}
