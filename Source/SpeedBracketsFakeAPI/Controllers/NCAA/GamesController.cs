using Microsoft.AspNetCore.Mvc;
using SpeedBracketsFakeAPI.Models;
using SpeedBracketsFakeAPI.Services;
using System;

namespace SpeedBracketsFakeAPI.Controllers.NCAA
{
	[Route("api/v1/ncaa/[controller]")]
	public class GamesController : ControllerBase
	{
		private readonly GameService gameService;
		private readonly ScheduleService scheduleService;

		public GamesController(GameService gameService, ScheduleService scheduleService)
		{
			this.gameService = gameService;
			this.scheduleService = scheduleService;
		}		
		
		[HttpGet("{gameId}/pbp")]
		public RealTimeEvent Pbp(string gameId)
		{
			return gameService.GetGameData(gameId);
		}

		[HttpGet("{gameId}/pbpEvent/{gameDelta?}")]
		public RealTimeEvent PbpEvent(string gameId, int gameDelta = 1)
		{
			return gameService.GetGameData(gameId, gameDelta);
		}

		[HttpGet("{gameId}/statistics.json")]
		public GameStatistic GetGameStatistics(string gameId)
		{
			return gameService.GetGameStatistics(gameId);
		}

		[HttpGet("{year}/{seasonType}/schedule.json")]
		public Schedule GetTournamentSchedule(int year, string seasonType)
		{
			return scheduleService.GetSchedule(year, seasonType);
		}
	}
}
